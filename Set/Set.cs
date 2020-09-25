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
            int booleanCount = (int)Math.Pow(2, Count);
            var boolean = new Set<Set<T>>();
            int bitMask;
            for (bitMask = 0; bitMask < booleanCount; ++bitMask)
            {
                boolean.Add(GetBooleanSubSet());
            }
            return boolean;

            Set<T> GetBooleanSubSet()
            {
                var subSet = new Set<T>();

                for (var it = this.GetEnumerator(); bitMask > 0;)
                {
                    it.MoveNext();
                    if ((bitMask & 1) == 1)
                    {
                        subSet.Add(it.Current);
                    }
                    bitMask >>= 1;
                }
                return subSet;
            }
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

        internal Set<Set<Set<T>>> GetAllPartitions()
        {
            var firstPartition = new Set<Set<T>>() { this };
            var result = new Set<Set<Set<T>>>() { firstPartition };
            MakeAllPartititionsOfCurrentSet(firstPartition);
            return result;

            void MakeAllPartititionsOfCurrentSet(Set<Set<T>> currentSet)
            {
                foreach (var set in currentSet)
                {
                    if (set.Count > 1)
                    {
                        var workSet = new Set<T>(set);

                        var rest = new Set<Set<T>>(currentSet);
                        rest.Remove(set);

                        if (workSet.Count < 2)
                            return;

                        var bitMask = new BitMask(workSet.Count);
                        while (bitMask.SetToNextCombination())
                        {
                            MakeSmallPartition(workSet, bitMask, out Set<T> leftSet, out Set<T> rightSet);

                            if ((leftSet.Count == 0) || (rightSet.Count == 0))
                            {
                                continue;
                            }

                            var smallSet = new Set<Set<T>>(rest);
                            smallSet.Add(leftSet);
                            smallSet.Add(rightSet);

                            if (result.Contains(smallSet))
                            {
                                continue;
                            }

                            result.Add(smallSet);

                            MakeAllPartititionsOfCurrentSet(smallSet);
                        }
                    }
                }
                static void MakeSmallPartition(Set<T> workSet, BitMask bitMask, out Set<T> leftSet, out Set<T> rightSet)
                {
                    var setEnum = workSet.GetEnumerator();
                    var bitMaskEnum = bitMask.GetEnumerator();

                    rightSet = new Set<T>();
                    leftSet = new Set<T>();
                    while (bitMaskEnum.MoveNext() && setEnum.MoveNext())
                    {
                        if (bitMaskEnum.Current)
                        {
                            leftSet.Add(setEnum.Current);
                        }
                        else
                        {
                            rightSet.Add(setEnum.Current);
                        }
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

            while (thisEnum.MoveNext() && otherEnum.MoveNext())
            {
                if (thisEnum.Current.CompareTo(otherEnum.Current) != 0)
                    return thisEnum.Current.CompareTo(otherEnum.Current);
            }
            return 0;
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", this) + "}";
        }
    }
}