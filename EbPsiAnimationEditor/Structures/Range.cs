using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EbPsiAnimationEditor.Structures
{
    // Normally this would be a struct, but the JSON reader seems to be allergic to structs
    public class Range
    {
        public readonly int Start;
        public readonly int Length;

        [JsonIgnore]
        public int End { get { return Start + Length - 1; } }

        public Range(int start, int length)
        {
            Start = start;
            Length = length;
        }

        public bool Contains(int point)
        {
            if (point >= this.Start && point < this.Start + this.Length)
                return true;

            return false;
        }

        public override string ToString()
        {
            return String.Format("Start: 0x{0:X}, Length: 0x{1:X}, End: 0x{2:X}",
                Start, Length, End);
        }
    }
}
