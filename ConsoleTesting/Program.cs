using System;
using System.Collections.Generic;
using System.Linq;
using CipherSolver.Ciphers;
using CipherSolver.Analysis;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "we have been discovered flee at once";

            foreach (string s in Frequency.BIGRAMS)
            {
                Console.WriteLine(s);
            }

            // Console.WriteLine(Caesar.Encrypt("abc", -3));

            Console.ReadKey();
        }
    }
}
