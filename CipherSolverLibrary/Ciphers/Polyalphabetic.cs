using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver.Analysis;

namespace CipherSolver.Ciphers
{
    public static class Polyalphabetic
    {
        /// <summary>
        /// Encrypts a given plaintext using a polyalphabetic cipher
        /// </summary>
        /// <returns>The encrypted message</returns>
        public static string Encrypt(string plaintext, string key)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < plaintext.Length; i++)
            {
                char c = plaintext[i];
                char k = key[i % key.Length];

                int newValue = Alphabet.IndexOf(c) + Alphabet.IndexOf(k);
                sb.Append(Alphabet.LetterAt(newValue % 26, true));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Decrypts a given ciphertext using a polyalphabetic cipher
        /// </summary>
        /// <returns>The decrypted plaintext</returns>
        public static string Decrypt(string ciphertext, string key)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < ciphertext.Length; i++)
            {
                char c = ciphertext[i];
                char k = key[i % key.Length];

                int newValue = Alphabet.IndexOf(c) - Alphabet.IndexOf(k);
                sb.Append(Alphabet.LetterAt((newValue + 26) % 26, false));
            }

            return sb.ToString();
        }
    }
}
