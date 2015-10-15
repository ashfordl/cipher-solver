using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CipherSolver.Analysis;

namespace CipherSolver.Ciphers
{
    public static class Caesar
    {
        /// <summary>
        /// Encrypts a given plaintext using a caesar shift cipher
        /// </summary>
        /// <returns>The encrypted message</returns>
        public static string Encrypt(string plaintext, int shift)
        {
            string output = "";

            foreach (char c in plaintext)
            {
                if (!Alphabet.IsAlphabetic(c))
                {
                    output += c;
                }
                else
                {
                    // Double mod, otherwise negative numbers cause issues
                    // -3 % 26 == -3 in C#, rather than the desired 23
                    int newVal = ((Alphabet.IndexOf(c) + shift) % 26 + 26) %  26;
                    output += Alphabet.LetterAt(newVal, true);
                }
            }

            return output;
        }

        /// <summary>
        /// Decrypts a given ciphertext using a caesar shift cipher
        /// </summary>
        /// <returns>The decrypted plaintext</returns>
        public static string Decrypt(string ciphertext, int shift)
        {
            string output = "";

            foreach (char c in ciphertext)
            {
                if (!Alphabet.IsAlphabetic(c))
                {
                    output += c;
                }
                else
                {
                    int newVal = ((Alphabet.IndexOf(c) - shift) % 26 + 26) % 26;
                    output += Alphabet.LetterAt(newVal, false);
                }
            }

            return output;
        }

        /// <summary>
        /// Decrpyts the ciphertext with every possible caesar cipher
        /// </summary>
        /// <param name="ciphertext">The ciphertext</param>
        /// <returns>A list of the decryptions, where the index is the shift</returns>
        public static List<string> DecryptAll(string ciphertext)
        {
            List<string> output = new List<string>();

            for (int i = 0; i < 26; i++)
            {
                output.Add(Caesar.Decrypt(ciphertext, i));
            }

            return output;
        }
    }
}
