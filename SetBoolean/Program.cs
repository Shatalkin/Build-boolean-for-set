using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace SetBoolean
{
    public static class Program
    {
        public static StringBuilder ToStringAsSet(this IEnumerable set, int deep = 0)
        {
            var s = new StringBuilder();
            var deeping = new string(Enumerable.Range(0, deep * 4).Select(x => ' ').ToArray());

            foreach (var item in set)
            {
                if (item is IEnumerable col)
                {
                    s.Append("\n" + deeping + "[");
                    s.Append(col.ToStringAsSet(deep + 1));
                    s.Append("]\n");
                }
                else
                {
                    var it = set.GetEnumerator();

                    while (it.MoveNext() == true)
                    {
                        s.Append(" " + it.Current + " ");
                    }
                    return s;
                }
            }
            return s;
        }

        private static void Main()
        {
            var i = 6;
            var set = Enumerable.Range(1, 3).Select(x => Enumerable.Range(i++, 2));
            var powerset = PowerSet(set);

            Console.WriteLine(powerset.ToStringAsSet());
        }

        private static IEnumerable<IEnumerable<T>> PowerSet<T>(IEnumerable<T> initialSet)
        {
            var boolean = new List<IEnumerable<T>>() { Enumerable.Empty<T>() } as IEnumerable<IEnumerable<T>>;

            return initialSet.Aggregate(boolean, (b, x) => (b.Concat(b.Select(z => z.Concat(new List<T>() { x })))));
        }
    }
}