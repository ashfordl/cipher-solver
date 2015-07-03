using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver;

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
