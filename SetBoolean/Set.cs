using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace SetBoolean
{
    internal class Set<T> : SortedSet<T>, IComparable<Set<T>>
        where T : IComparable<T>
    {
        public Set(IEnumerable<T> set)
            : base(set)
        {
        }

        public Set()
        {
        }

        internal Set<T> Union(Set<T> other)
        {
            var newSet = new Set<T>(this);
            foreach (var item in other)
            {
                newSet.Add(item);
            }
            return newSet;
        }

        internal Set<Set<T>> GetBoolean()
        {
            Set<T> GetBooleanSubSet(int bitMask)
            {
                var subSet = new Set<T>();

                for (var idx = this.GetEnumerator(); bitMask > 0;)
                {
                    idx.MoveNext();
                    if ((bitMask & 1) == 1)
                    {
                        subSet.Add(idx.Current);
                    }
                    bitMask >>= 1;
                }
                return subSet;
            }

            int booleanCount = (int)Math.Pow(2, Count);
            var boolean = new Set<Set<T>>();

            for (var bitMask = 0; bitMask < booleanCount; ++bitMask)
            {
                boolean.Add(GetBooleanSubSet(bitMask));
            }
            return boolean;
        }

        internal Set<Set<T>> GetBooleanGray()
        {
            var boolean = new Set<Set<T>>() { new Set<T>() };

            return this.Aggregate(boolean, (b, x) => new Set<Set<T>>(b.Concat(b.Select(z => new Set<T>(z.Concat(new Set<T>() { x }))))));
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", this) + "}";
        }

        public int CompareTo([AllowNull] Set<T> other)
        {
            if (Count.CompareTo(other.Count) != 0)
                return Count.CompareTo(other.Count);

            var thisEnum = this.GetEnumerator();
            var otherEnum = other.GetEnumerator();

            while (true)
            {
                thisEnum.MoveNext();
                otherEnum.MoveNext();

                if (thisEnum.Current.CompareTo(otherEnum.Current) != 0)
                    return thisEnum.Current.CompareTo(otherEnum.Current);
            }
        }
    }
}