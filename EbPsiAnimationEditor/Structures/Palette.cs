using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using EbPsiAnimationEditor.Tools;

namespace EbPsiAnimationEditor.Structures
{
    public sealed class Palette
    {
        // Colours
        public Color[] Colors { get; set; }

        public Palette()
        {
            Colors = new Color[4];
        }

        public Palette(Palette palette)
            : this()
        {
            for (int i = 0; i < 4; i++)
                this.Colors[i] = palette.Colors[i];
        }

        // Decodes a palette from a ROM
        public Palette(byte[] rom, int address)
            : this()
        {
            for (int i = 0; i < 4; i++)
            {
                Colors[i] = Palette.ReadColor(rom, address);
                address += 2;
            }
        }

        // Decodes a SNES colour from its encoded value
        public static Color DecodeColor(int value)
        {
            int r = (value & 0x1F) * 8;
            int g = ((value >> 5) & 0x1F) * 8;
            int b = ((value >> 10) & 0x1F) * 8;

            return Color.FromArgb(r, g, b);
        }

        public static Color ReadColor(byte[] rom, int address)
        {
            return DecodeColor(rom.ReadShort(address));
        }

        // Encodes a colour to its SNES-encoded representation
        public static int EncodeColor(Color color)
        {
            int temp = 0;

            temp |= ((color.R & 0xFF) / 8); ;
            temp |= (((color.G & 0xFF) / 8) << 5);
            temp |= (((color.B & 0xFF) / 8) << 10);

            return temp;
        }

        public static void WriteColor(byte[] rom, int address, Color color)
        {
            rom.WriteShort(address, EncodeColor(color));
        }

        // Writes this palette to a ROM
        public void Write(byte[] rom, int address)
        {
            for (int i = 0; i < Colors.Length; i++)
            {
                rom.WriteShort(address, Palette.EncodeColor(Colors[i]));
                address += 2;
            }
        }
    }
}
