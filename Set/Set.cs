using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Set
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

        internal Set<Set<T>> GetBooleanLib()
        {
            var boolean = new Set<Set<T>>() { new Set<T>() };

            return this.Aggregate(boolean, (b, x) => new Set<Set<T>>(b.Concat(b.Select(z => new Set<T>(z.Concat(new Set<T>() { x }))))));
        }

        internal Set<T> Intersect(Set<T> other)
        {
            var set = new Set<T>();

            foreach (var item in other)
            {
                if (this.Contains(item))
                    set.Add(item);
            }
            return set;
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

        internal Set<Tuple<T>> GetCartesianProduct<T1>(Set<T1> other)
            where T1 : IComparable<T1>
        {
            var result = new Set<Tuple<T>>();

            foreach (var thisItem in this)
            {
                foreach (var otherItem in other)
                {
                    var tuple = new Tuple<T>();

                    tuple.Add(thisItem);

                    if (otherItem is IEnumerable<T> otherTuple)
                    {
                        tuple.Add(otherTuple);
                    }
                    else if (otherItem is T otherValueItem)
                    {
                        tuple.Add(otherValueItem);
                    }
                    else
                    {
                        throw new ArgumentException("Sets must have same item type");
                    }

                    result.Add(tuple);
                }
            }

            return result;
        }

        internal Set<Set<Set<T>>> GetFullPartition()
        {
            var firstPartition = new Set<Set<T>>() { this };
            var result = new Set<Set<Set<T>>>() { firstPartition };
            GetPartitition(result, firstPartition);
            return result;

            void GetPartitition(Set<Set<Set<T>>> result, Set<Set<T>> currentSet)
            {
                foreach (var set in currentSet)
                {
                    if (set.Count > 1)
                    {
                        var current = new Set<T>(set);
                        var bitMask = new BitMask(current.Count);

                        var currentEnum = current.GetEnumerator();
                        var bitMaskEnum = bitMask.GetEnumerator();

                        var newSet = new Set<Set<T>>();

                        var rightSet = new Set<T>();
                        var leftSet = new Set<T>();

                        while (bitMaskEnum.MoveNext() && currentEnum.MoveNext())
                        {
                            if (bitMaskEnum.Current)
                            {
                                leftSet.Add(currentEnum.Current);
                            }
                            else
                            {
                                rightSet.Add(currentEnum.Current);
                            }
                        }

                        newSet.Add(rightSet);
                        newSet.Add(leftSet);

                        result.Add(newSet);

                        GetPartitition(result, newSet);

                        bitMask.NextBit();
                    }
                }
            }
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

        public override string ToString()
        {
            return "{" + string.Join(", ", this) + "}";
        }
    }
}