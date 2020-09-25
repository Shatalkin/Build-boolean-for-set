using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Set
{
    internal class BitMask : IEnumerable<bool>
    {
        private bool[] Mask { get; set; }

        public BitMask(int count)
        {
            Mask = new bool[count];
        }

        internal bool SetToNextCombination()
        {
            bool transfer = Mask[0];

            Mask[0] ^= true;

            int i;
            for (i = 1; transfer && (i < Mask.Length); ++i)
            {
                transfer = Mask[i];
                Mask[i] ^= true;
            }

            if (Mask.Any(x => x == true))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerator<bool> GetEnumerator()
        {
            foreach (var item in Mask)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Mask.GetEnumerator();
        }
    }
}