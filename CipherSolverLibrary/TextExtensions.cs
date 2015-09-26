using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver.Analysis;

namespace CipherSolver
{
    /// <summary>
    /// Contains various extension methods for working with strings and chars
    /// </summary>
    static class TextExtensions
    {
        /// <summary>
        /// Splits a string by the nth character and returns an array containing every offset substring
        /// </summary>
        /// <param name="s">The string to operate on</param>
        /// <param name="n">The number of arrays in which to split the string</param>
        /// <returns>An array of the substrings</returns>
        public static string[] SplitByNth(this string s, int n)
        {
            string[] output = new string[n];

            for (int i = 0; i < s.Length; i++)
            {
                output[i % n] += s[i];
            }

            return output;
        }

        public static string CombineSplitString(this IEnumerable<string> cols)
        {
            StringBuilder output = new StringBuilder();
            int longest = cols.Select(c => c.Length).Max();

            // Read characters across columns and append them to the output
            for (int r = 0; r < longest; r++)
            {
                foreach (string col in cols)
                {
                    if (r < col.Length)
                    {
                        output.Append(col[r]);
                    }
                }
            }

            return output.ToString();
        }
    }
}
