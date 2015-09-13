using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherSolver.Analysis
{
    public static class Frequency
    {
        /// <summary>
        /// The frequencies of each letter in ordinary English
        /// </summary>
        public static readonly Dictionary<char, double> NATURAL_FREQUENCIES = new Dictionary<char, double>();

        /// <summary>
        /// An ordered list of the most common bigrams in the English language (source Wikipedia)
        /// </summary>
        public static readonly List<string> BIGRAMS = new List<string>();

        static Frequency()
        {
            NATURAL_FREQUENCIES = FileReader.ReadLetterFrequencies();
            BIGRAMS = FileReader.ReadBigrams();
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

        /*public static int CountBigrams(string text)
        {

        }*/
    }
}
