using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CipherSolver
{
    public class RailFence
    {
        /// <summary>
        /// Encrypts a given message using the rail fence cipher
        /// </summary>
        /// <param name="plaintext">The message to encrypt</param>
        /// <param name="key">The length of the rail fence cipher</param>
        /// <returns>The encrypted message</returns>
        public static string Encrypt(string plaintext, int key)
        {
            string[] outputs = CreateRows(plaintext, key);

            // Combine all the outputs, reading accross the rows
            StringBuilder builder = new StringBuilder();

            foreach (string row in outputs)
            {
                builder.Append(row);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Creates rows in order to encrypt a plaintext
        /// </summary>
        /// <returns>The rows of the rail fence, top to bottom</returns>
        private static string[] CreateRows(string plaintext, int key)
        {
            // Set up variables
            int counter = 0;
            bool forward = false;
            string[] outputs = new string[key];

            // Iterate over each letter, appending it to the corresponding output
            foreach (char letter in plaintext)
            {
                outputs[counter] += letter;

                UpdateCounter(key, ref counter, ref forward);
            }
            return outputs;
        }

        /// <summary>
        /// Decrypts a rail fence cipher, given the cipher text and key length
        /// </summary>
        /// <returns>The decrypted plaintext</returns>
        public static string Decrypt(string ciphertext, int key)
        {
            // Find lengths for each row
            int[] lengths = FindLengths(ciphertext, key);

            // Construct plaintext and return
            return RebuildPlaintext(ciphertext, key, lengths);
        }

        /// <summary>
        /// Rebuilds the plaintext string, given the key length and lengths of each row.
        /// </summary>
        /// <param name="lengths">The lengths of each row, top to bottom</param>
        /// <returns>The plaintext string</returns>
        private static string RebuildPlaintext(string ciphertext, int key, int[] lengths)
        {
            // Split ciphertext into rows
            string[] rows = new string[key];
            for (int i = 0; i < lengths.Length; i++)
            {
                rows[i] = ciphertext.Substring(0, lengths[i]);
                ciphertext = ciphertext.Remove(0, lengths[i]);
            }

            // Iterate accross rows, traversing the rail fence to build output
            string output = "";
            int counter = 0;
            bool forward = false;
            while (CharsLeft(rows))
            {
                output += rows[counter][0];
                rows[counter] = rows[counter].Remove(0, 1);

                UpdateCounter(key, ref counter, ref forward);
            }

            return output;
        }

        /// <summary>
        /// Finds lengths of each row, given the key length
        /// </summary>
        /// <param name="ciphertext">The ciphertext</param>
        /// <param name="key">The key length</param>
        /// <returns>The lengths of each row, top to bottom</returns>
        private static int[] FindLengths(string ciphertext, int key)
        {
            // Number of chars fitting within a complete rail fence
            int completeLength = ciphertext.Length - (ciphertext.Length % ((2 * key) - 2));
            // Base length of top row, excluding left-overs
            int baseLength = completeLength / ((2 * key) - 2);

            // Set up basic lengths
            int[] lengths = new int[key];
            for (int i = 0; i < lengths.Length; i++)
            {
                // Top and bottom rows have only 1 char per rail fence
                if (i == 0 || i == lengths.Length - 1)
                {
                    lengths[i] = baseLength;
                }
                // Other rows have 2 chars per rail fence
                else
                {
                    lengths[i] = 2 * baseLength;
                }
            }

            // Add leftover chars to lengths
            int leftovers = ciphertext.Length - completeLength;
            int counter = 0;
            bool forward = false;
            while (leftovers > 0)
            {
                lengths[counter]++;
                leftovers--;

                UpdateCounter(key, ref counter, ref forward);
            }

            return lengths;
        }

        /// <summary>
        /// Updates the counter and forward variables to traverse the rail fence
        /// </summary>
        private static void UpdateCounter(int key, ref int counter, ref bool forward)
        {
            // Counter goes up and down to traverse up and down the rail fence
            if (counter == key - 1 || counter == 0)
            {
                forward = !forward;
            }

            counter = forward ? counter + 1 : counter - 1;
        }

        /// <summary>
        /// Returns true if the string array contains any non-0 length strings
        /// </summary>
        /// <param name="rows">The string array to check</param>
        /// <returns>False if and only if all the strings have length 0</returns>
        private static bool CharsLeft(string[] strings)
        {
            foreach (string str in strings)
            {
                if (str.Length != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
