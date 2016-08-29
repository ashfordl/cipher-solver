using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSolver.Commands
{
    static class CiphertextCommand
    {
        /// <summary>
        /// Prints the contents of CipherData.CipherText
        /// </summary>
        /// <param name="args">Ignored</param>
        public static void Run(List<string> args)
        {
            Console.WriteLine(CipherData.CipherText);
            Console.WriteLine("________________\nLength {0} chars", CipherData.CipherText.Length);
        }
    }
}
