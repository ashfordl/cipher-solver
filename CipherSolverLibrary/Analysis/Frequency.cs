using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Counts the frequency of the most common English bigrams in the text
        /// </summary>
        /// <param name="text">The text to analyse</param>
        /// <returns>The number of bigrams in the text</returns>
        public static int CountBigrams(string text)
        {
            text = text.ToLower();

            char prev = text[0];
            int total = 0;

            foreach (char c in text)
            {
                // Skip non-alphabetic characters
                if (!Alphabet.IsAlphabetic(c))
                {
                    continue;
                }

                // Create bigram for this iteration
                string bigram = prev.ToString() + c.ToString();

                if (BIGRAMS.Contains(bigram))
                {
                    total += 1;
                }

                prev = c;
            }

            return total;
        }

        /// <summary>
        /// Ranks a list of plaintexts by how 'English' they are
        /// </summary>
        /// <remarks>
        /// Bigrams are sorted by quantity. Frequency is ranked by how many of the
        /// top ten English letters are in the top ten letters of the given plaintext.
        /// Plaintexts are ranked for each of these metrics. The final order is created
        /// from the mean average of the individual ranks. If two plaintexts are matching,
        /// their order cannot be guaranteed.
        /// </remarks>
        /// <param name="plaintexts">The plaintexts to rank</param>
        /// <returns>The list of plaintexts sorted by the above methodology, with their stats.</returns>
        public static List<Tuple<string, int, int>> RankByFreqAndBigrams(List<string> plaintexts)
        {
            List<Tuple<string, int, int>> ranked = new List<Tuple<string, int, int>>();

            // The 10 most common letters in ordinary English
            var top10freqs = Frequency.NATURAL_FREQUENCIES
                                        .OrderByDescending(k => k.Value)
                                        .Select(kvp => kvp.Key.ToUpper())
                                        .Take(10);

            foreach (string plain in plaintexts)
            {
                var freqs = Analyse(plain)
                                .OrderByDescending(k => k.Value)            // Sort by descending frequency
                                .Take(10)                                   // Take the 10 most freq letters
                                .Where(kvp => kvp.Value > 0)                // Remove any letters with no counts
                                .Select(kvp => kvp.Key)                     // Throw away the countign data
                                .Where(letter => top10freqs.Contains(letter))   // Remove any letter not
                                .Count();                                       // appearing in English
                                                                                // top 10 letters

                ranked.Add(new Tuple<string, int, int>(plain, freqs, CountBigrams(plain)));
            }

            // The letter frequency score (Item2) is multiplied by 3, so it becomes a score out
            //  of 30 instead of 10, so that it can be weighted 50/50 with the bigrams score
            ranked = ranked.OrderByDescending(t => (t.Item2 * 3) + t.Item3)
                           .ToList();

            return ranked;
        }
    }
}
