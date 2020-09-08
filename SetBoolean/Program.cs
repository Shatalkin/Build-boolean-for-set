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
        private static string ToStringAsEnumerable<T>(this IEnumerable<T> seq)
        {
            return string.Join(", ", seq);
        }

        private static void Main()
        {
            var a = Enumerable.Range(1, 5);
            var b = Enumerable.Range(3, 5);

            var aUb = a.Union(b);
            var aCb = a.Concat(b);

            Console.WriteLine(nameof(aUb) + "=> " + aUb.ToStringAsEnumerable());
            Console.WriteLine(nameof(aCb) + "=> " + aCb.ToStringAsEnumerable());
        }
    }
}