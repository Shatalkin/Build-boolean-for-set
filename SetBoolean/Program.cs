using System;
using System.Collections.Generic;
using System.Linq;

namespace SetBoolean
{
    class Program
    {
        static IEnumerable<IEnumerable<T>> PowerSet<T>(IEnumerable<T> initialSet)
        {
            var boolean = new List<IEnumerable<T>>() { Enumerable.Empty<T>() } as IEnumerable<IEnumerable<T>>;

            return initialSet.Aggregate(boolean, (b, x) => b.Concat(b.Select(z => z.Concat(new List<T>() { x }))));
        }

        static void Main()
        {
            var set = Enumerable.Range(1, 4);
            var powerset = PowerSet(set);

            foreach (var subset in powerset)
            {
                foreach (var element in subset)
                    Console.Write("{0} ", element);
                Console.WriteLine();
            }
        }
    }
}
