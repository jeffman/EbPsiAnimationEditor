using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor.EB
{
    /// <summary>
    /// Represents the information associated with one PSI animation sequence
    /// Structure: https://github.com/PKHackers/EBYAML/blob/master/eb.yml#L20944
    /// </summary>
    public sealed class Animation
    {
        public Tileset Tileset { get; set; }
        public List<Frame> Frames { get; set; }
        public Palette Palette { get; set; }

        public int FrameInterval { get; set; }
        public int PaletteInterval { get; set; }
        public AnimationTarget Target { get; set; }

        public int EnemyColorDelay { get; set; }
        public int EnemyColorInterval { get; set; }
        public Color EnemyColor { get; set; }

        public int UnknownA { get; set; }
        public int UnknownB { get; set; }

        public Animation()
        {
            Frames = new List<Frame>();
        }

        public Animation(Animation anim)
            : this()
        {
            this.Tileset = anim.Tileset;
            for (int i = 0; i < anim.Frames.Count; i++)
                this.Frames.Add(new Frame(anim.Frames[i]));
            this.Palette = new Palette(anim.Palette);

            this.FrameInterval = anim.FrameInterval;
            this.PaletteInterval = anim.PaletteInterval;
            this.Target = anim.Target;

            this.EnemyColorDelay = anim.EnemyColorDelay;
            this.EnemyColorInterval = anim.EnemyColorInterval;
            this.EnemyColor = anim.EnemyColor;

            this.UnknownA = anim.UnknownA;
            this.UnknownB = anim.UnknownB;
        }

        public Animation(EBRom rom, int index, Dictionary<int, Tileset> tilesetCache)
            : this()
        {
            // Get the info address
            int infoAddress = rom.Config.Parameters["psi animation info"];
            infoAddress += (index * 12);

            // Get the frames address
            int framesAddress = rom.Config.Parameters["psi animation arrangements"];
            framesAddress = rom.Rom.ReadPointer(framesAddress + (index * 4));

            // Get the palette address
            int paletteAddress = rom.Config.Parameters["psi animation palettes"];
            paletteAddress += (index * 8);

            // Get the data

            // Tileset
            int tilesetPointer = rom.Rom.ReadShort(infoAddress) + 0xC0000;

            if (tilesetCache.ContainsKey(tilesetPointer))
                this.Tileset = tilesetCache[tilesetPointer];
            else
            {
                this.Tileset = Tileset.Decompress(rom.Rom, tilesetPointer, 256);
                tilesetCache.Add(tilesetPointer, this.Tileset);
            }

            // Frames
            int frameCount = rom.Rom[infoAddress + 6];
            byte[] arrangementData = Exhal.Decomp(rom.Rom, framesAddress);

            int expectedSize = 32 * 32 * frameCount;

            if (arrangementData.Length < expectedSize)
            {
                throw new Exception(String.Format(
                    "Decompressed frame data for animation {0} has an unexpected length: expected 0x{1:X} bytes, got 0x{2:X} bytes",
                    index, expectedSize, arrangementData.Length));
            }

            for (int j = 0; j < frameCount; j++)
                this.Frames.Add(new Frame(arrangementData, j * 32 * 32));

            // Palette
            this.Palette = new Palette(rom.Rom, paletteAddress);

            // Other stuff
            this.FrameInterval = rom.Rom[infoAddress + 2];
            this.PaletteInterval = rom.Rom[infoAddress + 3];
            this.UnknownA = rom.Rom[infoAddress + 4];
            this.UnknownB = rom.Rom[infoAddress + 5];

            int temp = rom.Rom[infoAddress + 7];
            if (temp > 3)
                throw new Exception(String.Format("Animation target out of range at ${0:X}: expected 0-3, got {1}",
                    infoAddress + 0xC00007, temp));

            this.Target = (AnimationTarget)temp;

            this.EnemyColorDelay = rom.Rom[infoAddress + 8];
            this.EnemyColorInterval = rom.Rom[infoAddress + 9];
            this.EnemyColor = Palette.ReadColor(rom.Rom, infoAddress + 10);
        }

        public Animation(byte[] importData, List<Tileset> tilesetCache)
        {
            if (importData.Length < 12)
                throw new Exception("Import data is not large enough to contain valid animation info");

            // Read the animation info   
            this.FrameInterval = importData[2];
            this.PaletteInterval = importData[3];
            this.UnknownA = importData[4];
            this.UnknownB = importData[5];
            int frameCount = importData[6];

            int temp = importData[7];
            if (temp > 3)
                throw new Exception(String.Format("Animation target out of range: expected 0-3, got {0}", temp));

            this.Target = (AnimationTarget)temp;

            this.EnemyColorDelay = importData[8];
            this.EnemyColorInterval = importData[9];
            this.EnemyColor = Palette.ReadColor(importData, 10);

            // Ensure that the data is the right size
            int expectedLength = (12 + 256 * 16 + frameCount * 32 * 32 + 8);
            if (importData.Length != expectedLength)
                throw new Exception(String.Format("Import data is the wrong size: expected {0} bytes, got {1} bytes",
                    expectedLength, importData.Length));

            // Read the tileset
            var tileset = new Tileset(importData, 12, 256);

            // Check if it's equal to any other tileset
            bool foundTileset = false;
            var comparer = new TilesetEqualityComparer();

            for (int i = 0; i < tilesetCache.Count; i++)
            {
                if (comparer.Equals(tileset, tilesetCache[i]))
                {
                    // Use the existing tileset instead
                    this.Tileset = tilesetCache[i];
                    foundTileset = true;
                    break;
                }
            }

            if (!foundTileset)
            {
                // Create a new tileset
                tilesetCache.Add(tileset);
                this.Tileset = tileset;
            }

            // Read the frames
            int address = 12 + 256 * 16;
            this.Frames = new List<Frame>();

            for (int i = 0; i < frameCount; i++)
                this.Frames.Add(new Frame(importData, address + (i * 32 * 32)));

            // Read the palette
            address += frameCount * 32 * 32;
            this.Palette = new Palette(importData, address);
        }

        public void Write(EBRom rom, int index, int framesAddress, Dictionary<Tileset, int> tilesetCache)
        {
            // Get the info address
            int infoAddress = rom.Config.Parameters["psi animation info"];
            infoAddress += (index * 12);

            // Get the frames address
            int framesPointer = rom.Config.Parameters["psi animation arrangements"];
            framesPointer += (index * 4);

            // Get the palette address
            int paletteAddress = rom.Config.Parameters["psi animation palettes"];
            paletteAddress += (index * 8);

            // Write the tileset pointer
            if (!tilesetCache.ContainsKey(this.Tileset))
                throw new Exception("Tileset not found in cache");

            int tilesetPointer = tilesetCache[this.Tileset];
            if (tilesetPointer < 0xC0000 || tilesetPointer > 0xCFFFF)
                throw new Exception("Tileset pointer must be between 0xC0000 and 0xCFFFF");

            rom.Rom.WriteShort(infoAddress, tilesetPointer - 0xC0000);

            // Write the frames pointer
            rom.Rom.WritePointer(framesPointer, framesAddress);

            // Write the palette
            Palette.Write(rom.Rom, paletteAddress);

            // Other stuff
            rom.Rom[infoAddress + 2] = (byte)FrameInterval;
            rom.Rom[infoAddress + 3] = (byte)PaletteInterval;
            rom.Rom[infoAddress + 4] = (byte)UnknownA;
            rom.Rom[infoAddress + 5] = (byte)UnknownB;
            rom.Rom[infoAddress + 6] = (byte)Frames.Count;
            rom.Rom[infoAddress + 7] = (byte)Target;
            rom.Rom[infoAddress + 8] = (byte)EnemyColorDelay;
            rom.Rom[infoAddress + 9] = (byte)EnemyColorInterval;
            Palette.WriteColor(rom.Rom, infoAddress + 10, EnemyColor);
        }

        public byte[] Export()
        {
            // Create a buffer
            byte[] buffer = new byte[
                12 +                            // Animation info
                256 * 16 +                      // Tileset
                this.Frames.Count * 32 * 32 +   // Frames
                8];                             // Palette

            // Write the info
            buffer[2] = (byte)this.FrameInterval;
            buffer[3] = (byte)this.PaletteInterval;
            buffer[4] = (byte)this.UnknownA;
            buffer[5] = (byte)this.UnknownB;
            buffer[6] = (byte)this.Frames.Count;
            buffer[7] = (byte)this.Target;
            buffer[8] = (byte)this.EnemyColorDelay;
            buffer[9] = (byte)this.EnemyColorInterval;
            Palette.WriteColor(buffer, 10, this.EnemyColor);

            // Write the tileset
            byte[] tilesetData = this.Tileset.Write();
            Array.Copy(tilesetData, 0, buffer, 12, tilesetData.Length);

            // Write the frames
            int address = 12 + tilesetData.Length;
            for (int i = 0; i < this.Frames.Count; i++)
                this.Frames[i].Write(buffer, address + i * 32 * 32);

            // Write the palette
            address += this.Frames.Count * 32 * 32;
            this.Palette.Write(buffer, address);

            return buffer;
        }

        public Bitmap RenderFrame(int frameIndex)
        {
            return Render.Frame(Frames[frameIndex], Tileset, Palette);
        }

        public Bitmap RenderFrameIndexed(int frameIndex)
        {
            return Render.FrameIndexed(Frames[frameIndex], Tileset, Palette);
        }
    }

    public enum AnimationTarget
    {
        OneEnemy = 0,
        Row = 1,
        AllEnemies = 2,
        Thunder = 3
    }
}
