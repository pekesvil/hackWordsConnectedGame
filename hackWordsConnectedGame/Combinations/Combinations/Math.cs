using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Combinations
{
    public static class Math
    {

        public static int CalcTotalPermutes(int n, int r)
        {
            int p = 0;

            p = Factorial(n) / (Factorial(n - r));

            return p;
        }

        private static int Factorial(int n)
        {
            int factorial = 1;
            for (int i = 1; i <= n; i++)
            {
                factorial *= i;
            }

            return factorial;
        }
    }
}
