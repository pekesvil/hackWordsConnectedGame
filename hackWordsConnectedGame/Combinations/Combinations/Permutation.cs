using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Combinations
{
     public static class Permutation
    {
        private static StringBuilder permutes = new StringBuilder();

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static string GetPermutation(char[] list, int group)
        {
            GetPermutations(list, 0, group);
            string result = permutes.ToString();
            permutes = new StringBuilder();
            return result;
        }

        private static void GetPermutations(char[] list, int k, int m)
        {
            if (k == m)
                setPermute(list);
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPermutations(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }

        private static void setPermute(char[] line)
        {
            string full_permutation = line.Aggregate("", (current, l) => current + l.ToString());
            permutes.AppendLine(full_permutation);

        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

    }
}


/*
         public static Permutation(string word, string CONNECTION)
        {
            _WORD = word;
            globalCounter = 0;
        }
     
     */
