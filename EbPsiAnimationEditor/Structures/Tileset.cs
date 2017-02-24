using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbPsiAnimationEditor.Tools;

namespace EbPsiAnimationEditor.Structures
{
    /// <summary>
    /// Represents a collection of TileBitmaps
    /// Really just an array wrapper
    /// </summary>
    public sealed class Tileset
    {
        public TileBitmap[] Bitmaps { get; private set; }

        public Tileset(int count)
        {
            Bitmaps = new TileBitmap[count];
        }

        public Tileset(Tileset tileset)
            : this(tileset.Bitmaps.Length)
        {
            for (int i = 0; i < this.Bitmaps.Length; i++)
                this.Bitmaps[i] = new TileBitmap(tileset.Bitmaps[i]);
        }

        public Tileset(byte[] data)
            : this(data, data.Length / 16)
        {

        }

        public Tileset(byte[] data, int lengthInTiles)
            : this(data, 0, lengthInTiles)
        {

        }

        public Tileset(byte[] rom, int address, int lengthInTiles)
            : this(lengthInTiles)
        {
            TileBitmap[] bitmaps = this.Bitmaps;

            for (int i = 0; i < lengthInTiles; i++)
            {
                bitmaps[i] = new TileBitmap(rom, address);
                address += 16;
            }
        }

        public static Tileset Decompress(byte[] rom, int address, int lengthInTiles)
        {
            byte[] tilesetData = DecompressTileset(rom, address);

            // Some frames use tile numbers outside the decompressed range
            // Temporary workaround: make each tileset the max size (256 tiles),
            // and trim empty tiles from the end when writing
            Array.Resize(ref tilesetData, lengthInTiles * 16);

            return new Tileset(tilesetData);
        }

        public static Tileset Decompress(byte[] rom, int address)
        {
            return new Tileset(DecompressTileset(rom, address));
        }

        private static byte[] DecompressTileset(byte[] rom, int address)
        {
            // Decompress a new tileset
            byte[] tilesetData = Exhal.Decomp(rom, address);

            // Verify that it's a multiple of 16 bytes
            if ((tilesetData.Length % 16) != 0)
                throw new Exception(String.Format(
                    "Tileset data at 0x{0:X} has an invalid length: expected a multiple of 0x10 bytes, got 0x{1:X} bytes",
                    address, tilesetData.Length));

            return tilesetData;
        }

        public byte[] Compress(bool fastCompress)
        {
            return Compress(Bitmaps.Length, fastCompress);
        }

        public byte[] Compress(int numTiles, bool fastCompress)
        {
            byte[] buffer = this.Write(numTiles);

            return Exhal.Comp(buffer, fastCompress);
        }

        public byte[] Write()
        {
            return this.Write(this.Bitmaps.Length);
        }

        public byte[] Write(int numTiles)
        {
            byte[] buffer = new byte[numTiles * 16];
            for (int i = 0; i < numTiles; i++)
                Bitmaps[i].Write(buffer, i * 16);

            return buffer;
        }
    }
}
