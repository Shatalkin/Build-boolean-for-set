using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Set
{
    internal class Relative<T> : Set<Tuple<T>>
        where T : IComparable<T>
    {
        private Func<T, T, bool> predicate;

        private Set<T> universum;

        public Relative()
        {
        }

        public Relative(IEnumerable<Tuple<T>> other)
            : base(other)
        {
        }

        public Relative(Set<T> universum, Func<T, T, bool> predicate)
        {
            this.universum = universum;
            this.predicate = predicate;

            foreach (var item in universum)
            {
                foreach (var secondItem in universum)
                {
                    if (predicate(item, secondItem))
                        this.Add(new Tuple<T>() { item, secondItem });
                }
            }
        }

        internal Relative<T> GetInversed()
        {
            var newRelative = new Relative<T>();
            foreach (var item in this)
            {
                newRelative.Add(new Tuple<T>((item as IEnumerable<T>).Reverse()));
            }
            return newRelative;
        }

        internal Relative<T> GetAddition()
        {
            var newRelative = new Relative<T>();
            foreach (var item in universum)
            {
                foreach (var secondItem in universum)
                {
                    if (!predicate(item, secondItem))
                        newRelative.Add(new Tuple<T>() { item, secondItem });
                }
            }
            return newRelative;
        }

        internal Relative<T> Compose(Relative<T> other)
        {
            var newRelative = new Relative<T>();
            foreach (var first in other)
            {
                foreach (var second in this)
                {
                    if (first[1].CompareTo(second[0]) == 0)
                    {
                        newRelative.Add(new Tuple<T> { first[0], second[1] });
                    }
                }
            }

            return newRelative;
        }
    }
}