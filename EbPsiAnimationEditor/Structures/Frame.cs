using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbPsiAnimationEditor.Tools;

namespace EbPsiAnimationEditor.Structures
{
    public class Frame
    {
        public int[,] Tiles { get; private set; }
        public int Width { get { return 32; } }
        public int Height { get { return 32; } }

        public Frame()
        {
            Tiles = new int[32, 32];

            for (int y = 0; y < 32; y++)
                for (int x = 0; x < 32; x++)
                    Tiles[x, y] = 0;
        }

        public Frame(Frame frame)
            : this()
        {
            for (int y = 0; y < 32; y++)
                for (int x = 0; x < 32; x++)
                    Tiles[x, y] = frame.Tiles[x, y];
        }

        public Frame(byte[] rom, int address)
            : this()
        {
            for (int y = 0; y < 32; y++)
                for (int x = 0; x < 32; x++)
                    Tiles[x, y] = rom[address++];
        }

        public void Write(byte[] rom, int address)
        {
            int[,] tiles = Tiles;

            for (int y = 0; y < 32; y++)
                for (int x = 0; x < 32; x++)
                    rom[address++] = (byte)tiles[x, y];
        }
    }
}
