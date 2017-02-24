using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace EbPsiAnimationEditor.Tools
{
    public static class Extensions
    {
        #region I/O

        public static int ReadInt(this byte[] rom, int address)
        {
            int temp = 0;

            temp |= rom[address++];
            temp |= (rom[address++] << 8);
            temp |= (rom[address++] << 16);
            temp |= (rom[address++] << 24);

            return temp;
        }

        public static void WriteInt(this byte[] rom, int address, int value)
        {
            rom[address++] = (byte)(value & 0xFF);
            rom[address++] = (byte)((value >> 8) & 0xFF);
            rom[address++] = (byte)((value >> 16) & 0xFF);
            rom[address++] = (byte)((value >> 24) & 0xFF);
        }

        public static int ReadShort(this byte[] rom, int address)
        {
            int temp = 0;

            temp |= rom[address++];
            temp |= (rom[address++] << 8);

            return temp;
        }

        public static void WriteShort(this byte[] rom, int address, int value)
        {
            rom[address++] = (byte)(value & 0xFF);
            rom[address++] = (byte)((value >> 8) & 0xFF);
        }

        public static int ReadPointer(this byte[] rom, int address)
        {
            return (rom.ReadInt(address) & 0xFFFFFF) - 0xC00000;
        }

        public static void WritePointer(this byte[] rom, int address, int pointer)
        {
            pointer += 0xC00000;

            rom[address++] = (byte)(pointer & 0xFF);
            rom[address++] = (byte)((pointer >> 8) & 0xFF);
            rom[address++] = (byte)((pointer >> 16) & 0xFF);
        }

        #endregion

        #region Other

        public static BitmapData LockBits(this Bitmap bitmap)
        {
            return bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
        }

        public static BitmapData LockBits(this Bitmap bitmap, int x, int y, int width, int height)
        {
            return bitmap.LockBits(new Rectangle(x, y, width, height),
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
        }

        public static void AddInt(this List<byte> list, int value)
        {
            list.Add((byte)(value & 0xFF));
            list.Add((byte)((value >> 8) & 0xFF));
            list.Add((byte)((value >> 16) & 0xFF));
            list.Add((byte)((value >> 24) & 0xFF));
        }

        public static void AddShort(this List<byte> list, int value)
        {
            list.Add((byte)(value & 0xFF));
            list.Add((byte)((value >> 8) & 0xFF));

        }

        public static void AddString(this List<byte> list, string value)
        {
            list.AddRange(Array.ConvertAll<char, byte>(value.ToCharArray(), c => (byte)c));
        }

        public static void Invoke(this Control control, Action action)
        {
            control.Invoke((Delegate)action);
        }

        #endregion
    }
}
