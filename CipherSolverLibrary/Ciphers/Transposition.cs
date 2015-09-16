using CipherSolver.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherSolverLibrary.Ciphers
{
    public static class Transposition
    {
        /// <summary>
        /// Encrypts a given plaintext using a columner transposition cipher
        /// </summary>
        /// <returns>The encrypted message</returns>
        public static string Encrypt(string plaintext, string key, bool removeRepeats = true)
        {
            List<int> keyData = GetKeyData(key, removeRepeats);
            List<string> columns = SplitToColumns(plaintext, keyData.Count);
            columns = OrderColumns();

            StringBuilder builder = new StringBuilder();
            foreach (string column in columns)
            {
                builder.Append(column);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Decrypts a given ciphertext using a columner transposition cipher
        /// </summary>
        /// <returns>The decrypted plaintext</returns>  
        public static string Decrypt(string ciphertext, string key, bool removeRepeats = true)
        {
            List<int> keyData = GetKeyData(key, removeRepeats);

            return string.Empty;
        }

        /// <summary>
        /// Convernts a key into key data to be used
        /// </summary>
        /// <returns>The key data</returns>
        private static List<int> GetKeyData(string key, bool removeRepeats = true)
        {
            if (removeRepeats)
            {
                key = Alphabet.RemoveDuplicates(key);
            }

            return Alphabet.StringToNumbers(key);
        }

        /// <summary>
        /// Splits a keytext into columns to be used
        /// </summary>
        /// <returns>A list of columns</returns>
        private static List<string> SplitToColumns(string plaintext, int keyLength)
        {
            StringBuilder[] builders = new StringBuilder[keyLength];
            for (int i = 0; i < keyLength; i++)
            {
                builders[i] = new StringBuilder();
            }

            for (int i = 0; i < plaintext.Length; i++)
            {
                builders[i % keyLength].Append(plaintext[i]);
            }

            List<string> res = builders.Select(b => b.ToString()).ToList();

            return res;
        }

        /// <summary>
        /// Orders the columns by the key
        /// </summary>
        /// <returns>The columns after being ordered</returns>
        private static List<string> OrderColumns(List<string> columns, List<int> key)
        {

        }
    }
}
