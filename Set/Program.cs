using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

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
            var set = new Set<int> { 1, 2, 3 };
            Console.WriteLine(set.GetAllPartitions());
        }
    }
}