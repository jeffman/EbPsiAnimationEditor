using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;
using System.Diagnostics;
using System.Drawing;

namespace EbPsiAnimationEditor.EB
{
    /// <summary>
    /// Represents an EB ROM and all relevant data parsed from it
    /// </summary>
    public sealed class EBRom
    {
        private static int[] headerlessSizes = {
                                    3 * 1024 * 1024,
                                    4 * 1024 * 1024,
                                    6 * 1024 * 1024
                                };

        private static int[] headerSizes = {
                                    headerlessSizes[0] + 512,
                                    headerlessSizes[1] + 512,
                                    headerlessSizes[2] + 512
                                };

        public RomConfig Config { get; set; }

        // Serialized headerless ROM
        public byte[] Rom { get; private set; }
        
        // Header, if applicable
        public byte[] Header { get; private set; }

        // PSI animation data blocks
        public Animation[] Animations { get; private set; }
        public List<Tileset> Tilesets { get; private set; }

        public EBRom(byte[] rom, byte[] header, RomConfig config)
        {
            Rom = rom;
            Header = header;
            Config = config;
        }

        public static bool Verify(byte[] rom, out string reason)
        {
            // Check for valid sizes
            if (!headerlessSizes.Contains(rom.Length) && !headerSizes.Contains(rom.Length))
            {
                reason = "ROM has an invalid size: 0x" + rom.Length.ToString("X");
                return false;
            }

            // Check for a header
            int headerOffset = 0;
            if (headerSizes.Contains(rom.Length))
                headerOffset = 512;

            // Check for the "EARTH BOUND" header
            byte[] ebHeader = "EARTH BOUND          ".Select(c => (byte)c).ToArray();
            int headerAddress = 0xFFC0 + headerOffset;
            for (int i = 0; i < ebHeader.Length; i++)
                if (rom[headerAddress++] != ebHeader[i])
                {
                    reason = "Not an EarthBound ROM (0xFFC0 header is missing)";
                    return false;
                }

            reason = "";
            return true;
        }

        public static EBRom FromArray(byte[] rom, RomConfig config)
        {
            // Extract the header
            byte[] header = null;
            if (headerSizes.Contains(rom.Length))
            {
                header = new byte[512];
                Array.Copy(rom, 0, header, 0, 512);

                byte[] tempRom = new byte[rom.Length - 512];
                Array.Copy(rom, 512, tempRom, 0, rom.Length - 512);

                rom = tempRom;
            }

            return new EBRom(rom, header, config);
        }

        // Deserializes all data from the Rom byte array
        public void ReadAll(IProgress<string> status, IProgress<int> progress)
        {
            Action<string> doStatus = (s) =>
                {
                    if (status != null)
                        status.Report(s);

                    if (progress != null)
                        progress.Report(0);
                };

            Action<int> doProgress = (i) =>
                {
                    if (progress != null)
                        progress.Report(i);
                };

            // Read the animations
            doStatus("Reading animations");

            int animationCount = Config.Parameters["psi animation count"];
            Animations = new Animation[animationCount];

            var tilesetCache = new Dictionary<int, Tileset>();
            for (int i = 0; i < animationCount; i++)
            {
                Animations[i] = new Animation(this, i, tilesetCache);
                doProgress(i * 100 / (animationCount - 1));
            }

            // Pull the tilesets into a list
            Tilesets = new List<Tileset>(tilesetCache.Values);
        }

        // Serializes all data to the Rom byte array
        public void WriteAll(IProgress<string> status, IProgress<int> progress, bool fastCompress, bool multiThread)
        {
            Action<string> doStatus = (s) =>
                {
                    if (status != null)
                        status.Report(s);

                    if (progress != null)
                        progress.Report(0);
                };

            Action<int> doProgress = (i) =>
                {
                    if (progress != null)
                        progress.Report(i);
                };

            // Multithreaded stuff
            int progressCounter = 0;
            var parallelOptions = new ParallelOptions();
            parallelOptions.MaxDegreeOfParallelism = Environment.ProcessorCount * 2;

            RangeCollection freeRanges = new RangeCollection(Config.FreeRanges);

            // Compress the tilesets first
            doStatus("Compressing tilesets");

            var compressedTilesets = new byte[Tilesets.Count][];
            var tilesetPointers = new int[Tilesets.Count];
            var tilesetCache = new Dictionary<Tileset, int>();

            Action<int> tilesetAction = i =>
            {
                compressedTilesets[i] = Exhal.Comp(Tilesets[i].Write(), fastCompress);
                int tilesetAddress = freeRanges.Allocate(compressedTilesets[i].Length, new Range(0xC0000, 0x10000));

                //if (tilesetAddress == -1)
                    //goto AllocationError;

                tilesetCache.Add(Tilesets[i], tilesetAddress);
                tilesetPointers[i] = tilesetAddress;

                doProgress((progressCounter++) * 100 / (Tilesets.Count - 1));
            };


            if (multiThread)
                Parallel.For(0, Tilesets.Count, tilesetAction);
            else
            {
                for (int i = 0; i < Tilesets.Count; i++)
                    tilesetAction(i);
            }

            // Now compress the frames
            var compressedFrames = new byte[Animations.Length][];
            var framesPointers = new int[Animations.Length];

            for (int i = 0; i < Animations.Length; i++)
            {
                Animation anim = Animations[i];
                var framesBuffer = new byte[anim.Frames.Count * 32 * 32];
                for (int j = 0; j < anim.Frames.Count; j++)
                    anim.Frames[j].Write(framesBuffer, j * 32 * 32);

                compressedFrames[i] = Exhal.Comp(framesBuffer, fastCompress);
                int framesAddress = freeRanges.Allocate(compressedFrames[i].Length);

                if (framesAddress == -1)
                    goto AllocationError;

                framesPointers[i] = framesAddress;
            }

            // Everything's good, so write it to the ROM
            for (int i = 0; i < Tilesets.Count; i++)
                Array.Copy(compressedTilesets[i], 0, Rom, tilesetPointers[i], compressedTilesets[i].Length);

            for (int i = 0; i < Animations.Length; i++)
            {
                Array.Copy(compressedFrames[i], 0, Rom, framesPointers[i], compressedFrames[i].Length);
                Animations[i].Write(this, i, framesPointers[i], tilesetCache);
            }

            return;

        AllocationError:
            throw new Exception("Could not allocate enough space for compressed data. Add more entries to the list of free ranges.");
        }

        public void Save(string filePath)
        {
            // Prepare the file buffer
            byte[] buffer;

            if (Header == null)
                buffer = Rom;

            else
            {
                buffer = new byte[Header.Length + Rom.Length];
                Array.Copy(Header, 0, buffer, 0, Header.Length);
                Array.Copy(Rom, 0, buffer, Header.Length, Rom.Length);
            }

            // Write
            File.WriteAllBytes(filePath, buffer);
        }
    }
}
