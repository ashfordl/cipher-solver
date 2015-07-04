using System;
using CipherSolver.Ciphers;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string ct = "abc";
            Console.WriteLine(RailFence.Decrypt(ct, 5));

            Console.ReadKey();
        }
    }
}
