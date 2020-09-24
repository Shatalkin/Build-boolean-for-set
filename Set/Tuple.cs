using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Set
{
    internal class Tuple<T> : List<T>, IComparable<Tuple<T>>
        where T : IComparable<T>
    {
        public Tuple()
        {
        }

        public Tuple(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public Tuple(int capacity)
            : base(capacity)
        {
        }

        internal void Add(IEnumerable<T> collection)
        {
            foreach (var item in collection)
                this.Add(item);
        }

        public int CompareTo([AllowNull] Tuple<T> other)
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
            return "(" + string.Join(", ", this) + ")";
        }
    }
}