using System;
using System.Collections.Generic;
using System.Text;

namespace Set
{
    internal class BitMask : List<bool>
    {
        internal bool NextBit()
        {
            bool transfer = this[0];

            this[0] ^= true;

            for (var i = 1; transfer; ++i)
            {
                if (i == this.Count)
                    return false;

                transfer = this[i];
                this[i] ^= true;
            }
            return true;
        }

        public BitMask(int count)
        {
            while (count > 0)
            {
                this.Add(false);
                --count;
            }
        }
    }
}