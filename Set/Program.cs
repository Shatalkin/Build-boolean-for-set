using System;
using System.Diagnostics.CodeAnalysis;

namespace Set
{
    public static class Program
    {
        private static string IEnumerableToString<T>(this IEquatable<T> collection)
        {
            return string.Join(',', collection);
        }

        private static void Main()
        {
            var set = new Set<int> { 1, 2, 3 };
            Console.WriteLine(set.GetFullPartition());
        }

        private class Point : IComparable<Point>
        {
            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            private int x;

            private int y;

            public override string ToString()
            {
                return $"( {x}, {y} )";
            }

            public int CompareTo([AllowNull] Point other)
            {
                if (this.x != other.x)
                    return this.x - other.x;
                return this.y - other.y;
            }
        }
    }
}