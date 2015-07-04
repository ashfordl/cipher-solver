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

            Console.WriteLine("--- FREQUENCY ANALYSIS RESULTS ---");

            foreach (KeyValuePair<char, int> data in Frequency.Analyse(text).OrderBy(key => key.Value))
            {
                Console.WriteLine(" {0}   {1,3}", data.Key, data.Value);
            }

            Console.ReadKey();
        }
    }
}
