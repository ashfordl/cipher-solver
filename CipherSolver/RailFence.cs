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
            // Set up variables
            int counter = 0;
            bool forward = false;
            string[] outputs = new string[key];

            // Iterate over each letter, appending it to the corresponding output
            foreach (char letter in plaintext)
            {
                outputs[counter] += letter;

                // Counter goes up and down to traverse up and down the rail fence
                if (counter == key - 1 || counter == 0)
                {
                    forward = !forward;
                }

                counter = forward ? counter + 1 : counter - 1;
            }

            // Combine all the outputs, reading accross the rows
            StringBuilder builder = new StringBuilder();

            foreach (string row in outputs)
            {
                builder.Append(row);
            }

            return builder.ToString();
        }

        public static string Decrypt(string ciphertext, int key)
        {
            // Number of chars fitting within a complete rail fence
            int completeLength = ciphertext.Length - (ciphertext.Length % ((2 * key) - 2));
            // Base length of top row, excluding left-overs
            int baseLength = completeLength / ((2 * key) - 2);

            // Set up basic lengths
            int[] lengths = new int[key];
            for (int i = 0; i < lengths.Length; i++)
            {
                if (i == 0 || i == lengths.Length - 1)
                {
                    lengths[i] = baseLength;
                }
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

                // Counter goes up and down to traverse up and down the rail fence
                if (counter == key - 1 || counter == 0)
                {
                    forward = !forward;
                }

                counter = forward ? counter + 1 : counter - 1;
            }

            // Split ciphertext into rows
            string[] rows = new string[key];
            for (int i = 0; i < lengths.Length; i++)
            {
                rows[i] = ciphertext.Substring(0, lengths[i]);
                ciphertext = ciphertext.Remove(0, lengths[i]);
            }

            // Iterate accross rows, traversing the rail fence to build output
            string output = "";
            counter = 0;
            forward = false;
            while(CharsLeft(rows))
            {
                output += rows[counter][0];
                rows[counter] = rows[counter].Remove(0, 1);

                // Counter goes up and down to traverse up and down the rail fence
                if (counter == key - 1 || counter == 0)
                {
                    forward = !forward;
                }

                counter = forward ? counter + 1 : counter - 1;
            }

            return output;
        }

        private static bool CharsLeft(string[] rows)
        {
            foreach (string row in rows)
            {
                if (row.Length != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
