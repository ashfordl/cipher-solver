using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherSolver.Analysis
{
    public static class Frequency
    {
        public static readonly Dictionary<char, double> NATURAL_FREQUENCIES = new Dictionary<char, double>();

        static Frequency()
        {
            // Letter frequencies in alphabetic order
            double[] freq = new double[]
            { 0.08167, 0.01492, 0.02782, 0.04253, 0.12702, 0.02228, 0.02015, 0.06094, 0.06966, 0.00153,
                0.00772, 0.04025, 0.02406, 0.06749, 0.07507, 0.01929, 0.00095, 0.05987, 0.06327, 0.09056,
                0.02758, 0.00978, 0.0236, 0.0015, 0.01974, 0.00074
            };

            // Add them all to dictionary
            for (int i = 0; i < freq.Length; i++)
            {
                NATURAL_FREQUENCIES.Add(Alphabet.LOWER[i], freq[i]);
            }
        }

        /// <summary>
        /// Performs frequency analysis on the text.
        /// </summary>
        /// <remarks>
        /// This method will ignore all non-alphabetic symbols and is case insensitive.
        /// </remarks>
        /// <param name="text">The text to analyse.</param>
        /// <returns>A dictionary of the analysis results.</returns>
        public static Dictionary<char, int> Analyse(string text)
        {
            Dictionary<char, int> results = new Dictionary<char,int>();

            // Create an entry in the dict for each letter in the alphabet
            foreach (char c in Alphabet.UPPER)
            {
                results.Add(c, 0);
            }

            // Count each letter in the text
            foreach (char c in text)
            {
                // Convert character to uppercase
                char letter = c.ToUpper();

                if (results.ContainsKey(letter))
                {
                    results[letter] += 1;
                }
            }

            return results;
        }
    }
}
