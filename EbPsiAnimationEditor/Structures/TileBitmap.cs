using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbPsiAnimationEditor.Tools;

namespace EbPsiAnimationEditor.Structures
{
    /// <summary>
    /// Represents an 8x8 2BPP SNES bitmap
    /// </summary>
    public sealed class TileBitmap
    {
        public byte[,] Pixels { get; private set; }

        public TileBitmap()
        {
            Pixels = new byte[8, 8];
        }

        public TileBitmap(TileBitmap tileBitmap)
            : this()
        {
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                    this.Pixels[x, y] = tileBitmap.Pixels[x, y];
        }

        public TileBitmap(byte[] rom, int address)
            : this()
        {
            byte[,] pixels = this.Pixels;

            for (int y = 0; y < 8; y++)
            {
                // Get the scanline for plane 0
                byte plane0 = rom[address++];

                // Get the scanline for plane 1
                byte plane1 = rom[address++];

                // Decode into a row of 2BPP pixel values
                for (int x = 0; x < 8; x++)
                {
                    int pixel = 0;

                    pixel |= ((plane0 >> x) & 1);
                    pixel |= (((plane1 >> x) & 1) << 1);

                    pixels[7 - x, y] = (byte)pixel;
                }
            }
        }

        public bool IsEmpty()
        {
            int sum = 0;
            for (int y = 0; y < 8; y++)
                for (int x = 0; x < 8; x++)
                    sum += Pixels[x, y];

            if (sum == 0) return true;

            return false;
        }

        public void Write(byte[] rom, int address)
        {
            byte[,] pixels = Pixels;

            for (int y = 0; y < 8; y++)
            {
                byte plane0 = 0;
                byte plane1 = 0;

                // Encode this row of pixels into the two scanlines
                for (int x = 0; x < 8; x++)
                {
                    int pixel = pixels[7 - x, y];

                    plane0 |= (byte)((pixel & 1) << x);
                    plane1 |= (byte)(((pixel >> 1) & 1) << x);
                }

                rom[address++] = plane0;
                rom[address++] = plane1;
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;
            byte[,] pixels = this.Pixels;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x += 4)
                {
                    hash ^= pixels[x, y];
                    hash ^= (pixels[x + 1, y] << 8);
                    hash ^= (pixels[x + 2, y] << 16);
                    hash ^= (pixels[x + 3, y] << 24);
                }
            }

            return hash;
        }
    }
}
