using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SetBoolean
{
    internal class Set<T> : IEnumerable<T>
    {
        internal IEnumerable<T> Collection { get; private set; }

        public Set(IEnumerable<T> set)
        {
            Collection = set;
        }

        internal Set<IEnumerable<T>> GetBoolean()
        {
            var boolean = new List<IEnumerable<T>>() { Enumerable.Empty<T>() } as IEnumerable<IEnumerable<T>>;

            return new Set<IEnumerable<T>>(this.Aggregate(boolean, (b, x) => b.Concat(b.Select(z => z.Concat(new List<T>() { x })))));
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