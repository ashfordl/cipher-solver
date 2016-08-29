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
        private static Dictionary<string, Action> Helps { get; set; }

        static Program()
        {
            RegisterCommands();
            CipherData.CipherText = "";
        }

        private static void RegisterCommands()
        {
            Commands = new Dictionary<string, Command>();
            Helps = new Dictionary<string, Action>();

            Commands.Add("read", ReadCommand.Run);
            Commands.Add("ciphertext", CiphertextCommand.Run);
            Commands.Add("cipher",     CiphertextCommand.Run);

            Helps.Add("read", ReadCommand.Help);
            Helps.Add("ciphertext", CiphertextCommand.Help);
            Helps.Add("cipher", CiphertextCommand.Help);
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
                    if (query.Count == 2 && query[1].ToUpper() == "-H")
                    {
                        Helps[query[0]]();
                    }
                    else
                    {
                        Commands[query[0]](query);
                    }
                }
                catch (Exception e)
                {
                    HandleCommandException(query, e);
                }
            }
        }

        /// <summary>
        /// Splits an input string into separate arguments
        /// </summary>
        private static List<string> SplitInputString(string input)
        {
            var args = new List<string>();

            // Split by whitespace
            args = input.Split(null as char[], StringSplitOptions.RemoveEmptyEntries).ToList();

            return args;
        }

        /// <summary>
        /// Handles if a command raises an uncaught exception
        /// </summary>
        /// <param name="query">The query inputted</param>
        /// <param name="e">The exception caught</param>
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

        /// <summary>
        /// Checks if the minimum number of arguments has been passed to the command
        /// </summary>
        /// <param name="args">The list of arguments</param>
        /// <param name="minimum">The minimum number of arguments required (excluding command name)</param>
        /// <returns>True if the sufficient number of arguments has been provided</returns>
        public static bool SufficientArgumentsCheck(List<string> args, int minimum)
        {
            return args.Count > minimum;
        }
    }
}
