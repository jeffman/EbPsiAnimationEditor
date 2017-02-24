using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

// Reference: http://www.matthewflickinger.com/lab/whatsinagif/bits_and_bytes.asp

namespace EbPsiAnimationEditor.Tools
{
    internal static class AnimatedGif
    {
        public static byte[] EncodeFrames(Bitmap[] frames, int frameDuration)
        {
            // Verify that all frames are 8bpp
            var validFrames = frames.Where(f => f.PixelFormat == PixelFormat.Format8bppIndexed);
            if (validFrames.Count() < frames.Length)
                throw new Exception("All frames must be 8bpp");

            // Verify that all frames are the same size
            var firstFrame = frames[0];
            var sameSizeFrames = frames.Skip(1).Where(f => f.Width == firstFrame.Width && f.Height == firstFrame.Height);
            if (sameSizeFrames.Count() < (frames.Length - 1))
                throw new Exception("All frames must be the same size");

            // Verify that all frames have the same palette
            var samePalFrames = frames.Skip(1).Where(f => ComparePalettes(firstFrame.Palette, f.Palette));
            if (samePalFrames.Count() < (frames.Length - 1))
                throw new Exception("All frames must have the same palette");

            // Create the output buffer
            var buffer = new List<byte>(frames.Length * firstFrame.Width * firstFrame.Height); // just an estimate

            // Write GIF header
            buffer.AddString("GIF89a");

            // Write logical screen descriptor
            buffer.AddShort(firstFrame.Width);
            buffer.AddShort(firstFrame.Height);
            buffer.Add(0xF7);
            buffer.Add(0x00);
            buffer.Add(0x00);

            // Write the global color table
            Color[] palette = firstFrame.Palette.Entries;
            for (int i = 0; i < palette.Length; i++)
            {
                buffer.Add(palette[i].R);
                buffer.Add(palette[i].G);
                buffer.Add(palette[i].B);
            }

            // Write the animation loop extension block
            buffer.Add(0x21);
            buffer.Add(0xFF);
            buffer.Add(0x0B);
            buffer.AddString("NETSCAPE2.0");
            buffer.Add(0x03);
            buffer.Add(0x01);
            buffer.Add(0x00);
            buffer.Add(0x00);
            buffer.Add(0x00);

            // Write the frames
            for (int i = 0; i < frames.Length; i++)
            {
                Bitmap frame = frames[i];

                // Write the control block
                buffer.Add(0x21);
                buffer.Add(0xF9);
                buffer.Add(0x04);
                buffer.Add(0x04);
                buffer.AddShort(frameDuration); // delay in 1/100ths of a second
                buffer.Add(0x00);
                buffer.Add(0x00);

                // Write the image descriptor block
                buffer.Add(0x2C);
                buffer.AddShort(0x00);
                buffer.AddShort(0x00);
                buffer.AddShort(frame.Width);
                buffer.AddShort(frame.Height);
                buffer.Add(0x00);

                // Tricky part -- get the LZW data by writing the frame to a temporary buffer using .NET
                using (var ms = new MemoryStream())
                {
                    frame.Save(ms, ImageFormat.Gif);

                    // Get LZW data
                    byte[] gifBuffer = ms.ToArray();
                    byte[] lzw = GetLzwData(gifBuffer);

                    buffer.AddRange(lzw);
                }
            }

            // Write the EOF
            buffer.Add(0x3B);

            return buffer.ToArray();
        }

        private static byte[] GetLzwData(byte[] buffer)
        {
            // Skip the 6-byte header
            int address = 6;

            // Get the screen descriptor flags
            byte flags = buffer[address + 4];
            
            // Ensure that there's a global color table
            if ((flags & 0x80) == 0)
                throw new Exception("Expected a global color table");

            // Get the color table size
            int colorTableSize = 1 << ((flags & 0x7) + 1);

            // Skip the logical screen descriptor
            address += 7;

            // Skip the global color table
            address += (colorTableSize * 3);

            // Look for the graphics control extension
            if (buffer.ReadShort(address) != 0xF921)
                throw new Exception("Expected a graphics control extension");

            // Skip it
            address += 8;

            // Look for the image descriptor
            if (buffer[address] != 0x2C)
                throw new Exception("Expected image descriptor");

            // Skip it
            address += 10;

            // Now we're at the LZW data -- skip the min code size
            int startAddress = address;
            address++;
                
            // Loop until we hit a null block
            while (buffer[address] != 0)
            {
                address += buffer[address] + 1;
            }

            address++;

            byte[] lzw = new byte[address - startAddress];
            Array.Copy(buffer, startAddress, lzw, 0, lzw.Length);

            return lzw;
        }

        private static bool ComparePalettes(ColorPalette first, ColorPalette second)
        {
            if (first.Entries.Length != second.Entries.Length)
                return false;

            for (int i = 0; i < first.Entries.Length; i++)
            {
                if (first.Entries[i] != second.Entries[i])
                    return false;
            }

            return true;
        }
    }
}
