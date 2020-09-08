using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
                        subSet.Collection.Add(Collection[collIdx]);
                    }
                    bitMask >>= 1;
                }
                return subSet;
            }

            var boolean = new Set<T>[(int)Math.Pow(2, Collection.Count)];

            for (int bitMask = 0; bitMask < boolean.Length; ++bitMask)
            {
                boolean[bitMask] = GetBooleanSubSet(bitMask) as Set<T>;
            }
            return new Set<Set<T>>(boolean);
        }

        internal Set<Set<T>> GetBooleanGray()
        {
            var boolean = new Set<Set<T>>() { new Set<T>(Enumerable.Empty<T>()) };

            return this.Aggregate(boolean, (b, x) => new Set<Set<T>>(b.Concat(b.Select(z => z.Concat(new Set<T>() { x }))).Select(x => new Set<T>(x))));
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append('{');
            foreach (var item in Collection)
            {
                if (item is IEnumerable coll)
                {
                    s.Append(string.Join(",\n", coll));
                }
                else
                {
                    s.Append(string.Join(", ", Collection));

                    break;
                }
            }
            s.Append("}\n");
            return s.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Collection.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}