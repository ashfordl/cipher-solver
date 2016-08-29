using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ConsoleSolver.Commands;

namespace ConsoleSolver
{
    public delegate void Command(List<string> args);

    class Program
    {
        private static Dictionary<string, Command> Commands { get; set; }

        static Program()
        {
            RegisterCommands();
        }


        private static void RegisterCommands()
        {
            Commands = new Dictionary<string, Command>();

            Commands.Add("read", ReadCommand.Run);
        }

        static void Main(string[] args)
        {
            // Commands are registered by the static constructor
            Console.WriteLine("ConsoleSolver");

            // Main input loop
            while (true)
            {
                Console.Write(">>>");
                string input = Console.ReadLine();
            }
        }

    }
}
