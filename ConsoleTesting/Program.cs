using System;
using CipherSolver.Ciphers;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string ct = "wecrlteerdsoeefeaocnwaivdeno";
            Console.WriteLine(RailFence.Decrypt(ct, 3));

            Console.ReadLine();
        }
    }
}
