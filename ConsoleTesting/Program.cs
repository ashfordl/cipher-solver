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
            string text = Console.ReadLine();

            var decrypted = Caesar.DecryptAll(text);

            for (int i = 0; i < 26; i++)
            {
                Console.WriteLine(decrypted[i]);
                Console.WriteLine(i);

                Console.WriteLine("\n\n\n");
                Console.ReadKey();
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
