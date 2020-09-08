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
        private static void Main()
        {
            Console.WriteLine(new Set<int>(Enumerable.Range(1, 3)).GetBoolean());

            //Console.WriteLine(set.GetBoolean().ToString());
        }
    }
}