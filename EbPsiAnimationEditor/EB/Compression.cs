using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbPsiAnimationEditor.Common;

namespace EbPsiAnimationEditor.EB
{
    /// <summary>
    /// Provides an interface to/from EB's data compression format
    /// Direct port of CoilSnake's implementation: https://github.com/kiij/CoilSnake/blob/master/coilsnake/util/eb/native_comp.c
    /// </summary>
    internal sealed class Compression
    {
        private static byte[] reverseBits;

        static Compression()
        {
            reverseBits = new byte[256];

            for (int i = 0; i < 256; i++)
            {
                int temp = i;

                temp = (((temp >> 1) & 0x55) | ((temp << 1) & 0xAA));
                temp = (((temp >> 2) & 0x33) | ((temp << 2) & 0xCC));
                temp = (((temp >> 4) & 0x0F) | ((temp << 4) & 0xF0));

                reverseBits[i] = (byte)temp;
            }
        }

        public static byte[] Decomp(byte[] rom, int address, out int bytesRead)
        {
            // Make a buffer
            byte[] buffer = new byte[128 * 1024];

            // Main loop
            int originalAddress = address;
            byte cdata;
            int bpos = 0;
            int bpos2 = 0;
            int tmp = 0;
            int prevAddress = address;
            int prevPrevAddress = address;

            while ((cdata = rom[address]) != 0xFF)
            {
                if(address==0xb)
                {

                }

                prevPrevAddress = prevAddress;
                prevAddress = address;

                int cmdtype = cdata >> 5;
                int len = (cdata & 0x1F) + 1;

                if (cmdtype == 7)
                {
                    cmdtype = (cdata & 0x1C) >> 2;
                    len = ((cdata & 3) << 8) + rom[++address] + 1;
                }

                if (bpos + len > buffer.Length)
                    throw new Exception();

                address++;

                if (cmdtype >= 4)
                {
                    bpos2 = (rom[address] << 8) + rom[address + 1];
                    address += 2;
                }

                switch (cmdtype)
                {
                    case 0:
                        Array.Copy(rom, address, buffer, bpos, len);
                        address += len;
                        bpos += len;
                        break;

                    case 1:
                        for (int i = 0; i < len; i++)
                            buffer[bpos++] = rom[address];

                        address++;
                        break;

                    case 2:
                        if (bpos + (2 * len) > buffer.Length)
                            throw new Exception();

                        while (len-- > 0)
                        {
                            buffer.WriteShort(bpos, rom.ReadShort(address));
                            bpos += 2;
                        }

                        address += 2;
                        break;

                    case 3:
                        tmp = rom[address++];

                        while (len-- > 0)
                            buffer[bpos++] = (byte)(tmp++);

                        break;

                    case 4:
                        if (bpos2 + len > buffer.Length) throw new Exception();

                        //Array.Copy(buffer, bpos2, buffer, bpos, len);
                        int tempLen = len;
                        while (tempLen-- > 0)
                            buffer[bpos++] = buffer[bpos2++];

                        //bpos += len;
                        break;

                    case 5:
                        if (bpos2 + len > buffer.Length) throw new Exception();

                        while (len-- > 0)
                            buffer[bpos++] = reverseBits[buffer[bpos2++]];

                        break;

                    case 6:
                        if (bpos2 - len + 1 > buffer.Length)
                            throw new Exception();

                        while (len-- > 0)
                            buffer[bpos++] = buffer[bpos2--];

                        break;

                    case 7:
                        throw new Exception();

                    default:
                        throw new Exception();
                }
            }

            byte[] decompressed = new byte[bpos];
            Array.Copy(buffer, decompressed, bpos);

            bytesRead = address - originalAddress;

            return decompressed;
        }

        private static void Encode(byte[] dest, ref int bpos, int length, int type)
        {
            if (length > 32)
            {
                dest[bpos++] = (byte)(0xE0 + 4 * type + ((length - 1) >> 8));
                dest[bpos++] = (byte)((length - 1) & 0xFF);
            }
            else
            {
                dest[bpos++] = (byte)(0x20 * type + length - 1);
            }
        }

        private static void Rencode(byte[] dest, ref int bpos, byte[] source, int pos, int length)
        {
            if (length <= 0)
                return;

            Encode(dest, ref bpos, length, 0);
            Array.Copy(source, pos, dest, bpos, length);
            bpos += length;
        }

        public static int Comp(byte[] source, int sourcePos, byte[] dest, int destPos, int length)
        {
            int bpos = destPos;
            int limit = sourcePos + length;
            int pos = sourcePos;
            int pos2, pos3, pos4;
            int tmp;

            while (pos < limit)
            {
                for (pos2 = pos; pos2 < limit && pos2 < pos + 1024; pos2++)
                {
                    for (pos3 = pos2; pos3 < limit && pos3 < pos2 + 1024 && source[pos2] == source[pos3]; pos3++) ;

                    if (pos3 - pos2 >= 3)
                    {
                        Rencode(dest, ref bpos, source, pos, pos2 - pos);
                        Encode(dest, ref bpos, pos3 - pos2, 1);
                        dest[bpos++] = source[pos2];
                        pos = pos3;
                        break;
                    }

                    for (pos3 = pos2; pos3 < limit && pos3 < pos2 + 2048 && source[pos3] == source[pos2] && source[pos3 + 1] == source[pos2 + 1]; pos3 += 2) ;

                    if (pos3 - pos2 >= 6)
                    {
                        Rencode(dest, ref bpos, source, pos, pos2 - pos);
                        Encode(dest, ref bpos, (pos3 - pos2) / 2, 2);
                        dest[bpos++] = source[pos2];
                        dest[bpos++] = source[pos2 + 1];
                        pos = pos3;
                        break;
                    }

                    for (tmp = 0, pos3 = pos2; pos3 < limit && pos3 < pos2 + 1024 && source[pos3] == source[pos2] + tmp; pos3++, tmp++) ;

                    if (pos3 - pos2 >= 4)
                    {
                        Rencode(dest, ref bpos, source, pos, pos2 - pos);
                        Encode(dest, ref bpos, pos3 - pos2, 3);
                        dest[bpos++] = source[pos2];
                        pos = pos3;
                        break;
                    }

                    for (pos3 = sourcePos; pos3 < pos2; pos3++)
                    {
                        for (tmp = 0, pos4 = pos3; pos4 < pos2 && tmp < 1024 && pos2 + tmp < limit && source[pos4] == source[pos2 + tmp]; pos4++, tmp++) ;
                        //for (tmp = 0, pos4 = pos3; pos4 < pos2 && tmp < 1024 && source[pos4] == source[pos2 + tmp]; pos4++, tmp++) ;

                        if (tmp >= 5)
                        {
                            Rencode(dest, ref bpos, source, pos, pos2 - pos);
                            Encode(dest, ref bpos, tmp, 4);
                            dest[bpos++] = (byte)((pos3 - sourcePos) >> 8);
                            dest[bpos++] = (byte)((pos3 - sourcePos) & 0xFF);
                            pos = pos2 + tmp;
                            goto DONE;
                        }

                        for (tmp = 0, pos4 = pos3; pos4 < pos2 && tmp < 1024 && pos2 + tmp < limit && source[pos4] == reverseBits[source[pos2 + tmp]]; pos4++, tmp++) ;
                        //for (tmp = 0, pos4 = pos3; pos4 < pos2 && tmp < 1024 && source[pos4] == reverseBits[source[pos2 + tmp]]; pos4++, tmp++) ;

                        if (tmp >= 5)
                        {
                            Rencode(dest, ref bpos, source, pos, pos2 - pos);
                            Encode(dest, ref bpos, tmp, 5);
                            dest[bpos++] = (byte)((pos3 - sourcePos) >> 8);
                            dest[bpos++] = (byte)((pos3 - sourcePos) & 0xFF);
                            pos = pos2 + tmp;
                            goto DONE;
                        }

                        for (tmp = 0, pos4 = pos3; pos4 >= sourcePos && tmp < 1024 && pos2 + tmp < limit &&source[pos4] == source[pos2 + tmp]; pos4--, tmp++) ;
                        //for (tmp = 0, pos4 = pos3; pos4 >= sourcePos && tmp < 1024 && source[pos4] == source[pos2 + tmp]; pos4--, tmp++) ;

                        if (tmp >= 5)
                        {
                            Rencode(dest, ref bpos, source, pos, pos2 - pos);
                            Encode(dest, ref bpos, tmp, 6);
                            dest[bpos++] = (byte)((pos3 - sourcePos) >> 8);
                            dest[bpos++] = (byte)((pos3 - sourcePos) & 0xFF);
                            pos = pos2 + tmp;
                            goto DONE;
                        }
                    }
                }

            DONE:
                Rencode(dest, ref bpos, source, pos, pos2 - pos);
                if (pos < pos2) pos = pos2;

            }

            dest[bpos++] = 0xFF;
            return bpos - destPos;
        }

        public static int Comp2(byte[] source, int sourcePos, byte[] dest, int destPos, int length)
        {
            int runWindow = 1024;
            int backWindow = 128;

            // Max block length = 1024 (2048 in the case of type 2)
            // Max intra-buffer address is 65535 (we can essentially reference anywhere in the buffer)

            // Type 0: copy len bytes from cdata to buffer
            // Type 1: read one byte from cdata, write the same byte len times to buffer
            // Type 2: read a short from cdata, write the same shirt len times to buffer
            // Type 3: read one byte from cdata, write the byte to buffer len times, incrementing its value each time
            // Type 4: copy len bytes from buffer to itself (from bpos2 to bpos)
            // Type 5: same as case 5, but with reversed bits
            // Type 6: copy len bytes in reverse order from buffer to itself (from bpos2 to bpos)

            int originalPos = destPos;
            int[] gains = new int[6];
            int[] lengths = new int[6];
            int[] bufferSkips = new int[6];
            bool directCopy = false;
            int directPos = 0;
            int directLength = 0;

            for (int uPos = sourcePos; uPos < (sourcePos + length); )
            {
                //if (destPos == 0x14)
                {

                }

                int counter;
                int checkLimit;

                // Type 1: check for same byte occurring multiple times
                int temp = source[uPos];

                checkLimit = Math.Min(runWindow, sourcePos + length - uPos);
                for (counter = 1; counter < checkLimit; counter++)
                {
                    if (source[uPos + counter] != temp)
                        break;
                }

                bufferSkips[0] = counter;
                gains[0] = counter - 2;
                if (counter > 32) gains[0]--;
                lengths[0] = counter;

                // Type 2: check for same short occurring multiple times
                if (length - uPos + sourcePos > 1)
                {
                    temp = source.ReadShort(uPos);

                    checkLimit = Math.Min(runWindow * 2, sourcePos + length - uPos) & 0x7FFFFFFE;
                    for (counter = 2; counter < checkLimit; counter += 2)
                    {
                        if (source.ReadShort(uPos + counter) != temp)
                            break;
                    }

                    bufferSkips[1] = counter;
                    gains[1] = counter - 3;
                    if (counter > 64) gains[1]--;
                    lengths[1] = counter / 2;
                }
                else
                {
                    bufferSkips[1] = -1;
                    gains[1] = -1;
                }

                // Type 3: check for ramp data
                temp = source[uPos];

                checkLimit = Math.Min(runWindow, sourcePos + length - uPos);
                for (counter = 1; counter < checkLimit; counter++)
                {
                    if (source[uPos + counter] != ++temp)
                        break;
                }

                bufferSkips[2] = counter;
                gains[2] = counter - 2;
                if (counter > 32) gains[2]--;
                lengths[2] = counter;

                // The remaining command types use a backwards reference, so we need to keep track of those
                int[] backRefs = new int[3];

                // Type 4: check for direct copy in buffer
                int bufferPos = -1;
                bool found = false;
                int lowerLimit = bufferSkips.Take(3).Max();

                for (counter = Math.Min(sourcePos + length - uPos - 1, Math.Min(backWindow, uPos - sourcePos)); counter > lowerLimit; counter--)
                {
                    //if(counter==0x14)
                    {

                    }
                    // Buffer position to check
                    for (bufferPos = sourcePos; bufferPos < uPos; bufferPos++)
                    {
                        //if(bufferPos==0x14a)
                        {

                        }

                        // Check for a pattern of length 'counter' starting at 'bufferPos'
                        found = true;
                        int counter2;

                        for (counter2 = 0; counter2 < counter && bufferPos + counter2 < uPos; counter2++)
                        {
                            if (source[uPos + counter2] != source[bufferPos + counter2])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (counter2 != counter)
                            found = false;

                        if (found)
                            break;
                    }

                    if (found)
                        break;
                }

                if (found)
                {
                    bufferSkips[3] = counter;
                    gains[3] = counter - 3;
                    if (counter > 32) gains[3]--;
                    lengths[3] = counter;
                    backRefs[0] = bufferPos;
                }
                else
                {
                    bufferSkips[3] = -1;
                    gains[3] = -1;
                    backRefs[0] = -1;
                    lengths[3] = -1;
                }

                // Type 5: check for direct copy in buffer using bit reversals
                found = false;
                lowerLimit = Math.Max(lowerLimit, bufferSkips[3]);

                for (counter = Math.Min(sourcePos + length - uPos - 1, Math.Min(backWindow, uPos - sourcePos)); counter > lowerLimit; counter--)
                {
                    // Buffer position to check
                    for (bufferPos = sourcePos; bufferPos < uPos; bufferPos++)
                    {
                        // Check for a pattern of length 'counter' starting at 'bufferPos'
                        found = true;
                        int counter2;

                        for (counter2 = 0; counter2 < counter && bufferPos + counter2 < uPos; counter2++)
                        {
                            if (reverseBits[source[uPos + counter2]] != source[bufferPos + counter2])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (counter2 != counter)
                            found = false;

                        if (found)
                            break;
                    }

                    if (found)
                        break;
                }

                if (found)
                {
                    bufferSkips[4] = counter;
                    gains[4] = counter - 3;
                    if (counter > 32) gains[4]--;
                    lengths[4] = counter;
                    backRefs[1] = bufferPos;
                }
                else
                {
                    bufferSkips[4] = -1;
                    gains[4] = -1;
                    lengths[4] = -1;
                    backRefs[1] = -1;
                }

                // Type 6: check for direct copy in buffer in reverse order
                found = false;
                lowerLimit = Math.Max(lowerLimit, bufferSkips[4]);

                for (counter = Math.Min(sourcePos + length - uPos - 1, Math.Min(backWindow, uPos - sourcePos)); counter > lowerLimit; counter--)
                {
                    // Buffer position to check
                    for (bufferPos = sourcePos + counter; bufferPos < uPos; bufferPos++)
                    {
                        // Check for a pattern of length 'counter' starting at 'bufferPos'
                        found = true;
                        int counter2;

                        for (counter2 = 0; counter2 < counter && bufferPos - counter2 >= 0; counter2++)
                        {
                            if (reverseBits[source[uPos + counter2]] != source[bufferPos - counter2])
                            {
                                found = false;
                                break;
                            }
                        }

                        if (counter2 != counter)
                            found = false;

                        if (found)
                            break;
                    }

                    if (found)
                        break;
                }

                if (found)
                {
                    bufferSkips[5] = counter;
                    gains[5] = counter - 3;
                    if (counter > 32) gains[5]--;
                    lengths[5] = counter;
                    backRefs[2] = bufferPos;
                }
                else
                {
                    bufferSkips[5] = -1;
                    gains[5] = -1;
                    lengths[5] = -1;
                    backRefs[2] = -1;
                }

                // Check which mode gives us the most gain
                int maxIndex = 0;
                int maxGain = gains[0];

                for (int i = 1; i < 6; i++)
                {
                    if (gains[i] > maxGain)
                    {
                        maxGain = gains[i];
                        maxIndex = i;
                    }
                }

                if (maxGain > 0)
                {
                    // We found a pattern with positive gain
                    // Flush the direct copy if necessary
                    if (directCopy)
                    {
                        directCopy = false;
                        Encode2(dest, ref destPos, 0, directLength);
                        Array.Copy(source, directPos, dest, destPos, directLength);
                        destPos += directLength;

                        directPos = 0;
                        directLength = 0;
                    }

                    // Encode the pattern
                    Encode2(dest, ref destPos, maxIndex + 1, lengths[maxIndex]);

                    switch (maxIndex)
                    {
                        case 0:
                        case 2:
                            // Repeat one byte or increment current byte
                            dest[destPos++] = source[uPos];
                            break;

                        case 1:
                            // Repeat short
                            dest.WriteShort(destPos, source.ReadShort(uPos));
                            destPos += 2;
                            break;

                        case 3:
                        case 4:
                        case 5:
                            // Back reference
                            //dest.WriteShort(destPos, backRefs[maxIndex - 3]);
                            dest[destPos++] = (byte)((backRefs[maxIndex - 3] >> 8) & 0xFF);
                            dest[destPos++] = (byte)(backRefs[maxIndex - 3] & 0xFF);
                            break;

                        default:
                            break;
                    }

                    uPos += bufferSkips[maxIndex];
                }

                else
                {
                    // Direct copy
                    if (!directCopy)
                    {
                        directCopy = true;
                        directPos = uPos;
                    }

                    directLength++;
                    uPos++;
                }
            }

            dest[destPos++] = 0xFF;
            return destPos - originalPos;
        }

        private static void Encode2(byte[] dest, ref int destPos, int type, int length)
        {
            if (length > 32)
            {
                dest[destPos++] = (byte)(0xE0 + (type << 2) + (((length - 1) >> 8) & 3));
                dest[destPos++] = (byte)((length - 1) & 0xFF);
            }
            else
            {
                dest[destPos++] = (byte)((type << 5) + ((length - 1) & 0x1F));
            }
        }
    }
}
