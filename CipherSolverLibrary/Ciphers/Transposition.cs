using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                output.Append(relevantCols.CombineSplitString());

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

            builder.Append(cols.CombineSplitString());

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
        /// Splits the ciphertext into columns for decrypting
        /// </summary>
        /// <param name="ciphertext">The ciphertext to split</param>
        /// <param name="keyLength">The length of the key</param>
        /// <returns>A list of the columns</returns>
        private static string[] CiphertextToColumns(string ciphertext, List<int> keyPos)
        {
            int baselen = ciphertext.Length / keyPos.Count;
            // The number of characters appended onto the end of columns
            int excess = ciphertext.Length - (baselen * keyPos.Count);
            // Finds the length corresponding to each column
            var colLens = keyPos.Select((val, i) => new { Value = val, Length = excess > i ? baselen + 1 : baselen });

            string[] cols = new string[keyPos.Count];

            // Iterate once for each unique key letter
            for (int i = 0; i < keyPos.Count; i++)
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
