﻿using System;
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
                var query = SplitInputString(input);

                if (query.Count < 1)
                {
                    continue;
                }
                else if (!Commands.ContainsKey(query[0]))
                {
                    Console.WriteLine("Command not found");
                    continue;
                }

                try
                {
                    Commands[query[0]](query);
                }
                catch (Exception e)
                {
                    HandleCommandException(query, e);
                }

                Console.WriteLine();

            }
        }

        private static List<string> SplitInputString(string input)
        {
            var args = new List<string>();

            // Split by whitespace
            args = input.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).ToList();

            return args;
        }

        private static void HandleCommandException(List<string> query, Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n\n'{0}' command failed with {1}", query[0], e.GetType().ToString());
            Console.WriteLine("Print debug info? Y/N");
            do
            {
                var key = Console.ReadLine().ToString().ToUpper();

                if (key == "Y")
                {
                    Console.WriteLine("{1}: {0}\n{2}", e.Message, e.GetType().Name, e.StackTrace);
                    break;
                }
                else if (key == "N")
                {
                    break;
                }

                Console.Write("ok");
            } while (true);

            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
