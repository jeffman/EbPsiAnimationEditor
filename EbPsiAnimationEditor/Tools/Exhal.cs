using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace EbPsiAnimationEditor.Tools
{
    internal static class Exhal
    {
        [DllImport("compress", EntryPoint = "pack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Comp(byte[] unpacked, int inputsize, byte[] packed, int fast);

        [DllImport("compress", EntryPoint = "unpack", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Decomp(byte[] packed, int inputoffset, byte[] unpacked, int outputoffset, ref int bytesread);

        public static int Decomp(byte[] packed, int inputoffset, byte[] unpacked, int outputoffset)
        {
            int temp = 0;
            return Exhal.Decomp(packed, inputoffset, unpacked, outputoffset, ref temp);
        }

        public static byte[] Decomp(byte[] packed, int inputoffset)
        {
            byte[] buffer = new byte[64 * 1024];
            int newSize = Exhal.Decomp(packed, inputoffset, buffer, 0);
            Array.Resize(ref buffer, newSize);
            return buffer;
        }

        public static byte[] Comp(byte[] unpacked, bool fastCompress)
        {
            byte[] buffer = new byte[64 * 1024];
            int compSize = Comp(unpacked, unpacked.Length, buffer, fastCompress ? 1 : 0);
            Array.Resize(ref buffer, compSize);
            return buffer;
        }
    }
}
