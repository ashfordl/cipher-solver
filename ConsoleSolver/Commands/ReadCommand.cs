using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSolver.Commands
{
    static class ReadCommand
    {
        /// <summary>
        /// Reads the contents of the given file and stores in CipherData.CipherText
        /// </summary>
        /// <param name="args">Expected 1, the file to read from</param>
        public static void Run(List<string> args)
        {
            if (args.Count != 2)
            {
                Console.WriteLine("This command expects 1 argument: the filepath to read the ciphertext from.");
                return;
            }

            string path = args[1];

            if (path.ToUpper() == "-H" || path.ToUpper() == "--HELP")
            {
                Console.WriteLine("This command will read the contents of a file and store it as the ciphertext"
                                +" on which other commands may operate.");
                Console.WriteLine("Format: read <filepath>\t<filepath> may be relative or absolute.");
                return;
            }

            string contents = "";

            try
            {
                contents = File.ReadAllText(path);
            }
            catch
            {
                Console.WriteLine("Error reading file. Check the file exists and this application has read priviliges.");
                return;
            }

            Console.WriteLine("Contents of {0} read successfully:", path);
            Console.WriteLine(contents);
            CipherData.CipherText = contents;
        }
    }
}
