using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbPsiAnimationEditor.Structures
{
    public sealed class RangeCollection
    {
        public List<Range> Ranges { get; private set; }
        private object allocationLock = new object();

        public RangeCollection()
        {
            Ranges = new List<Range>();
        }

        public RangeCollection(RangeCollection ranges)
            : this()
        {
            this.Ranges.AddRange(ranges.Ranges);
        }

        public void Add(Range range)
        {
            // Ensure that this range has positive length
            if (range.Length < 1)
                throw new Exception("Range must have positive length");

            // Ensure that this range does not overlap with any other range
            foreach (var r in Ranges)
            {
                if (r.Contains(range.Start) || r.Contains(range.End) ||
                    range.Contains(r.Start) || range.Contains(r.End))
                {
                    throw new Exception("Ranges may not overlap");
                }
            }

            // Insert it at the appropriate spot
            int i = 0;
            for (i = 0; i < Ranges.Count; i++)
            {
                Range r = Ranges[i];
                if (range.Start >= r.End)
                {
                    i++;
                    break;
                }
            }

            Ranges.Insert(i, range);
        }

        public void Modify(int index, Range range)
        {
            // Ensure that this range has positive length
            if (range.Length < 1)
                throw new Exception("Range must have positive length");

            // Ensure that this range does not overlap with any other range
            for (int i = 0; i < Ranges.Count; i++)
            {
                if (i == index) continue;

                var r = Ranges[i];
                if (r.Contains(range.Start) || r.Contains(range.End) ||
                    range.Contains(r.Start) || range.Contains(r.End))
                {
                    throw new Exception("Ranges may not overlap");
                }
            }

            Ranges[index] = range;
        }

        public void Clear()
        {
            Ranges.Clear();
        }

        public int Allocate(int length)
        {
            return Allocate(length, new Range(0, 0x7FFFFFFF));
        }

        public int Allocate(int length, Range preferredRange)
        {
            lock (allocationLock)
            {
                for (int i = 0; i < Ranges.Count; i++)
                {
                    if (Ranges[i].Length >= length && preferredRange.Contains(Ranges[i].Start) &&
                        preferredRange.Contains(Ranges[i].End))
                    {
                        int address = Ranges[i].Start;
                        Ranges[i] = new Range(Ranges[i].Start + length, Ranges[i].Length - length);
                        return address;
                    }
                }
            }

            return -1;
        }
    }
}
