using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using EbPsiAnimationEditor.Tools;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor.EB
{
    internal static unsafe class Render
    {
        public static Bitmap Frame(Frame arrangement, Tileset tileset, Palette palette)
        {
            // Create the canvas
            Bitmap canvas = new Bitmap(arrangement.Width * 8, arrangement.Height * 8, PixelFormat.Format32bppArgb);
            BitmapData bData = canvas.LockBits();
            int* pixelData = (int*)bData.Scan0;
            int pixelStride = bData.Stride / 4;
            int* highestAddress = pixelData + (pixelStride * canvas.Height);

            var tiles = arrangement.Tiles;
            var tileBitmaps = tileset.Bitmaps;

            int arrangementWidth = arrangement.Width;
            int arrangementHeight = arrangement.Height;

            // Pre-cast the palettes to integer equivalents
            int[] intPalette = PaletteToInts(palette);

            // Render
            for (int tileY = 0; tileY < arrangementHeight; tileY++)
            {
                for (int tileX = 0; tileX < arrangementWidth; tileX++)
                {
                    int tile = tiles[tileX, tileY];
                    byte[,] pixels = tileBitmaps[tile].Pixels;

                    int* startPointer = pixelData + (tileY * pixelStride * 8) + (tileX * 8);

                    for (int pixelY = 0; pixelY < 8; pixelY++)
                    {
                        int* pixelPointer = startPointer;

                        for (int pixelX = 0; pixelX < 8; pixelX++)
                        {
                            *pixelPointer = intPalette[pixels[pixelX, pixelY]];
                            pixelPointer++;
                        }

                        startPointer += pixelStride;
                    }
                }
            }

            canvas.UnlockBits(bData);
            return canvas;
        }

        public static void Tile(Bitmap canvas, int tileIndex, int tileX, int tileY, Tileset tileset, Palette palette)
        {
            // Only lock the necessary region
            BitmapData bData = canvas.LockBits(tileX * 8, tileY * 8, 8, 8);

            byte[,] pixels = tileset.Bitmaps[tileIndex].Pixels;
            int[] paletteInts = PaletteToInts(palette);

            int pixelStride = bData.Stride / 4;
            int* startPointer = (int*)bData.Scan0;

            for (int pixelY = 0; pixelY < 8; pixelY++)
            {
                int* pixelPointer = startPointer;

                for (int pixelX = 0; pixelX < 8; pixelX++)
                {
                    *pixelPointer = paletteInts[pixels[pixelX, pixelY]];
                    pixelPointer++;
                }

                startPointer += pixelStride;
            }

            canvas.UnlockBits(bData);
        }

        public static Bitmap Tile(TileBitmap tile, Palette palette)
        {
            Bitmap canvas = new Bitmap(8, 8, PixelFormat.Format32bppArgb);
            BitmapData bData = canvas.LockBits();

            byte[,] pixels = tile.Pixels;
            int[] paletteInts = PaletteToInts(palette);

            int pixelStride = bData.Stride / 4;
            int* startPointer = (int*)bData.Scan0;

            for (int pixelY = 0; pixelY < 8; pixelY++)
            {
                int* pixelPointer = startPointer;

                for (int pixelX = 0; pixelX < 8; pixelX++)
                {
                    *pixelPointer = paletteInts[pixels[pixelX, pixelY]];
                    pixelPointer++;
                }

                startPointer += pixelStride;
            }

            canvas.UnlockBits(bData);

            return canvas;
        }

        public static Bitmap FrameIndexed(Frame arrangement, Tileset tileset, Palette palette)
        {
            Bitmap canvas = new Bitmap(arrangement.Width * 8, arrangement.Height * 8, PixelFormat.Format8bppIndexed);
            BitmapData bData = canvas.LockBits();
            byte* pixelData = (byte*)bData.Scan0;
            int pixelStride = bData.Stride;
            byte* highestAddress = pixelData + (pixelStride * canvas.Height);

            var tiles = arrangement.Tiles;
            var tileBitmaps = tileset.Bitmaps;

            int arrangementWidth = arrangement.Width;
            int arrangementHeight = arrangement.Height;

            // Set the palette
            ColorPalette canvasPal = canvas.Palette;
            for (int i = 0; i < palette.Colors.Length; i++)
            {
                canvasPal.Entries[i] = palette.Colors[i];
            }
            canvas.Palette = canvasPal;

            // Render
            for (int tileY = 0; tileY < arrangementHeight; tileY++)
            {
                for (int tileX = 0; tileX < arrangementWidth; tileX++)
                {
                    int tile = tiles[tileX, tileY];
                    byte[,] pixels = tileBitmaps[tile].Pixels;

                    byte* startPointer = pixelData + (tileY * pixelStride * 8) + (tileX * 8);

                    for (int pixelY = 0; pixelY < 8; pixelY++)
                    {
                        byte* pixelPointer = startPointer;

                        for (int pixelX = 0; pixelX < 8; pixelX++)
                        {
                            *pixelPointer = pixels[pixelX, pixelY];
                            pixelPointer++;
                        }

                        startPointer += pixelStride;
                    }
                }
            }

            canvas.UnlockBits(bData);
            return canvas;
        }

        private static int[] PaletteToInts(Palette palette)
        {
            var ints = new int[palette.Colors.Length];

            for (int j = 0; j < ints.Length; j++)
            {
                ints[j] = palette.Colors[j].ToArgb();
            }

            return ints;
        }
    }
}
