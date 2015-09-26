using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver;
using CipherSolver.Analysis;

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
            var orderedKey = keyData.OrderBy(v => v).ToList();
            // Finds the order to read columns (eg tomato = 421042)
            var keyPos = keyData.Select((val, ind) => orderedKey.IndexOf(val)).ToList();

            string[] cols = plaintext.SplitByNth(key.Length);
            StringBuilder output = new StringBuilder();

            // Loop through each unique key value
            for (int k = 0; k < key.Length; k++)
            {
                // Select columns with this key letter
                var relevantCols = cols.Where((c, i) => keyPos[i] == k)
                                       .Select(c => c);

                int longest = relevantCols.Select(c => c.Length).Max();

                // Read characters across columns and append them to the output
                for (int r = 0; r < longest; r++)
                {
                    foreach (string col in relevantCols)
                    {
                        if(r < col.Length)
                        {
                            output.Append(col[r]);
                        }
                    }
                }

                // Skip ahead if there were repeated key letters
                k += relevantCols.Count() - 1;
            }

            return output.ToString().ToUpper();
        }

        /// <summary>
        /// Decrypts a given ciphertext using a columner transposition cipher
        /// </summary>
        /// <returns>The decrypted plaintext</returns>  
        public static string Decrypt(string ciphertext, string key, bool removeRepeats = true)
        {
            List<int> keyData = GetKeyData(key, removeRepeats);
            var orderedKey = keyData.OrderBy(v => v).ToList();
            // Finds the order to read columns (eg tomato = 421042)
            var keyPos = keyData.Select((val, ind) => orderedKey.IndexOf(val)).ToList();

            // Split the ciphertext into columns
            string[] cols = Transposition.CiphertextToColumns(ciphertext, keyPos);

            StringBuilder builder = new StringBuilder();

            int longest = cols.Select(c => c.Length).Max();

            // Loop through rows then through columns and build the plaintext
            for (int i = 0; i < longest; i++)
            {
                for (int j = 0; j < cols.Length; j++)
                {
                    if (i < cols[j].Length)
                    {
                        builder.Append(cols[j][i]);
                    }
                }
            }

            return builder.ToString().ToLower();
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
        private static List<string> PlaintextToColumns(string plaintext, int keyLength)
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
        /// Splits the ciphertext into columns for decrypting
        /// </summary>
        /// <param name="ciphertext">The ciphertext to split</param>
        /// <param name="keyLength">The length of the key</param>
        /// <returns>A list of the columns</returns>
        private static string[] CiphertextToColumns(string ciphertext, List<int> keyPos)
        {
            int keyLength = keyPos.Count;
            int baselen = ciphertext.Length / keyLength;
            // The number of characters appended onto the end of columns
            int excess = ciphertext.Length - (baselen * keyLength);
            // Finds the length corresponding to each column
            var colLens = keyPos.Select((val, i) => new { Value = val, Length = excess > i ? baselen + 1 : baselen });

            string[] cols = new string[keyLength];

            // Iterate once for each unique key letter
            for (int i = 0; i < keyLength; i++)
            {
                // Finds the columns headered by this key letter
                var relevantColumns = colLens.Where(len => len.Value == i)
                                                .Select(c => c.Length);
                
                int totalLength = relevantColumns.Sum();
                int count = relevantColumns.Count();

                // Split the relevant ciphertext into the two columns
                string[] splitCols = ciphertext.Substring(0, totalLength).SplitByNth(count);

                // Remove the used ciphertext from the remaining ciphertext
                ciphertext = ciphertext.Substring(totalLength);

                // Add the columns created to the output
                foreach (string column in splitCols)
                {
                    int index = keyPos.IndexOf(i);
                    cols[index] = column;
                    keyPos[index] = -1;
                }

                i += count - 1;
            }

            return cols;
        }
    }
}
