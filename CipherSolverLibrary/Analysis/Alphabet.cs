using System;
using System.Collections.Generic;

namespace CipherSolver.Analysis
{
    public static class Alphabet
    {
        public const string UPPER = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LOWER = "abcdefghijklmnopqrstuvwxyz";

        /// <summary>
        /// Returns true if the character is alphabetic, ignoring case.
        /// </summary>
        public static bool IsAlphabetic(char c)
        {
            return UPPER.Contains(new string(c.ToUpper(), 1));
        }

        /// <summary>
        /// Returns the index of the character within the alphabet.
        /// </summary>
        /// <param name="c">The character to find.</param>
        /// <returns>The index of the character.</returns>
        public static int IndexOf(char c)
        {
            string ch = new string(c.ToUpper(), 1);

            if (UPPER.Contains(ch))
            {
                return UPPER.IndexOf(ch);
            }

            throw new ArgumentException("Parameter c must be an alphabetic character", "c");
        }

        /// <summary>
        /// Returns the character at the specified index in the alphabet.
        /// </summary>
        /// <param name="index">The character index.</param>
        /// <param name="uppercase">True if the character should be upper case, false if lowercase.</param>
        /// <returns>The character at that index.</returns>
        public static char LetterAt(int index, bool uppercase)
        {
            if (uppercase)
            {
                return UPPER[index];
            }
            else
            {
                return LOWER[index];
            }
        }

        /// <summary>
        /// Returns a string converted a list of each letter's integer value.
        /// </summary>
        /// <param name="s">The string to convert to integer values.</param>
        public static List<int> StringToNumbers(string s)
        {

        }

        /// <summary>
        /// Returns a string with repeated characters removed.
        /// </summary>
        /// <param name="s">The string to remove repeats from.</param>
        public static string RemoveRepeats(string s)
        {

        }

        /// <summary>
        /// Extension method for characters to provide ToUpper functionality.
        /// </summary>
        public static char ToUpper(this char ch)
        {
            return new string(ch, 1).ToUpper()[0];
        }

        /// <summary>
        /// Extension method for characters to provide ToLower functionality.
        /// </summary>
        public static char ToLower(this char ch)
        {
            return new string(ch, 1).ToLower()[0];
        }
    }
}
