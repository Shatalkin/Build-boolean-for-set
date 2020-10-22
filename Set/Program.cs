using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Set
{
    public static class Program
    {
        private static string IEnumerableToString<T>(this IEnumerable<T> collection)
        {
            return string.Join(',', collection);
        }

        private static void Main()
        {
            var set = new Set<int> { 1, 2, 3, 4 };
            var rel1 = new Relative<int>(set, (x, y) => x == y - 1);
            var rel2 = new Relative<int>(set, (x, y) => x < y);
            Console.WriteLine(rel1);
            Console.WriteLine(rel2);
            Console.WriteLine(rel1.Compose(rel2));
        }
    }
}