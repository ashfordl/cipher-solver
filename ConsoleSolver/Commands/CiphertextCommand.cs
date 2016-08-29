using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver;

namespace ConsoleSolver.Commands
{
    static class CiphertextCommand
    {
        public static void Help()
        {
            Console.WriteLine("If no arguments are provided, the current value of the working"
                            + " ciphertext will be printed. Alternatively, this value may be updated"
                            + " with set. The ciphertext may also be 'cleaned' - non-alphabetic"
                            + " characters removed and all characters made uppercase.");
            Console.WriteLine("Format: cipher set <new value>\t<new value> may contain any character.");
            Console.WriteLine("        cipher clean");
        }

        /// <summary>
        /// Prints the contents of CipherData.CipherText
        /// </summary>
        /// <param name="args">Ignored</param>
        public static void Run(List<string> args)
        {
            if (args.Count == 1)
            {
                Console.WriteLine(CipherData.CipherText);
                Console.WriteLine("________________\nLength {0} chars", CipherData.CipherText.Length);
                return;
            }

            string arg2 = args[1].ToUpper();

            if (arg2 == "SET")
            {
                // Reset ciphertext
                CipherData.CipherText = "";
                // Remove the first two arguments (cipher & set) so they are not included
                args.RemoveRange(0, 2);
                // Append each chunk to the ciphertext, restoring spaces
                args.ForEach(s => CipherData.CipherText += (" " + s));
                // Remove leading space from first loop iteration
                CipherData.CipherText = CipherData.CipherText.Remove(0, 1);
            }
            else if (arg2 == "CLEAN")
            {
                CipherData.CipherText = new string(CipherData.CipherText.Where(c => c.IsAlphabetic())
                                                                        .Select(c => c.ToUpper())
                                                                        .ToArray());
            }
            else
            {
                Help();
            }
        }
    }
}
