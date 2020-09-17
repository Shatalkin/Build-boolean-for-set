using System;
using System.Diagnostics.CodeAnalysis;

namespace SetBoolean
{
    public static class Program
    {
        private static void Main()
        {
            var set = new Set<Point>(){
                new Point(1,1),
                new Point(2,3),
                new Point(0,0)
            };
            Console.WriteLine(set.GetBooleanGray());
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