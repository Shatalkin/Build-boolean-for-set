using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace SetBoolean
{
    internal class Set<T> : IEnumerable<T>
    {
        internal List<T> Collection { get; private set; }

        public Set()
        {
            Collection = new List<T>();
        }

        public Set(IEnumerable<T> set)
        {
            Collection = set.ToList();
        }

        public Set(int capacity)
        {
            Collection = new List<T>(capacity);
        }

        internal void Add(T item)
        {
            Collection.Add(item);
        }

        internal Set<Set<T>> GetBoolean()
        {
            Set<T> GetBooleanSubSet(int bitMask)
            {
                var subSet = new Set<T>();

                for (var collIdx = 0; (collIdx < Collection.Count) && (bitMask > 0); collIdx++)
                {
                    if ((bitMask & 1) == 1)
                    {
                        subSet.Add(Collection[collIdx]);
                    }
                    bitMask >>= 1;
                }
                return subSet;
            }

            var booleanCount = (int)Math.Pow(2, Collection.Count);
            var boolean = new Set<Set<T>>(booleanCount);

            for (int bitMask = 0; bitMask < booleanCount; ++bitMask)
            {
                boolean.Add(GetBooleanSubSet(bitMask));
            }
            return new Set<Set<T>>(boolean);
        }

        internal Set<Set<T>> GetBooleanGray()
        {
            var boolean = new Set<Set<T>>() { new Set<T>() };

            return this.Aggregate(boolean, (b, x) => new Set<Set<T>>(b.Concat(b.Select(z => new Set<T>(z.Concat(new Set<T>() { x }))))));
        }

        public T this[int idx]
        {
            get
            {
                return Collection[idx];
            }
            set
            {
                Collection[idx] = value;
            }
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", this) + "}";
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}