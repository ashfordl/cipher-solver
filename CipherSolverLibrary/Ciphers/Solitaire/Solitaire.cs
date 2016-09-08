using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherSolver.Ciphers.Solitaire
{
    public static class Solitaire
    {
        /// <summary>
        /// Encrypts a given plaintext using a solitaire cipher.
        /// </summary>
        /// <param name="d"> The deck to use for the encryption. </param>
        /// <returns>The encrypted message</returns>
        public static string Encrypt(string plainText, ref Deck d)
        {
            string keystream = d.GenerateKeystream(plainText.Length);
            return Polyalphabetic.Encrypt(plainText, keystream);
        }

        /// <summary>
        /// Decrypts a given ciphertext using a solitaire cipher.
        /// </summary>
        /// <param name="d"> The deck to use for the decryption. </param>
        /// <returns>The decrypted message</returns>
        public static string Decrypt(string plainText, ref Deck d)
        {
            string keystream = d.GenerateKeystream(plainText.Length);
            return Polyalphabetic.Decrypt(plainText, keystream);
        }
    }
}
