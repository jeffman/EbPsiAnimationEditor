using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbPsiAnimationEditor.Structures;

namespace EbPsiAnimationEditor.Structures
{
    public sealed class TilesetEqualityComparer : IEqualityComparer<Tileset>
    {
        public bool Equals(Tileset a, Tileset b)
        {
            // Check if two tilesets have the same bitmaps
            if (a == null || b == null)
                return false;

            if (a.Bitmaps.Length != b.Bitmaps.Length)
                return false;

            for (int i = 0; i < a.Bitmaps.Length; i++)
            {
                byte[,] first = a.Bitmaps[i].Pixels;
                byte[,] second = b.Bitmaps[i].Pixels;

                for (int y = 0; y < 8; y++)
                    for (int x = 0; x < 8; x++)
                        if (first[x, y] != second[x, y])
                            return false;
            }

            return true;
        }

        public int GetHashCode(Tileset obj)
        {
            int hash = 0;

            for (int i = 0; i < obj.Bitmaps.Length; i++)
                hash ^= obj.Bitmaps[i].GetHashCode();

            return hash;
        }
    }
}
