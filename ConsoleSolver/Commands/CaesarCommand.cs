using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherSolver.Analysis;
using CipherSolver.Ciphers;

namespace ConsoleSolver.Commands
{
    static class CaesarCommand
    {
        public static void Help()
        {
            Console.WriteLine("The caesar command will either shift the current ciphertext by the given shift"
                             + " or will try all possible shifts, and rank them by how 'english' they seem to be.");
            Console.WriteLine("Format: caesar <shift>");
            Console.WriteLine("        caesar try");
        }

        /// <summary>
        /// Either shifts the current ciphertext by the given amount, or tries all and returns analysis
        /// data upon them.
        /// </summary>
        /// <param name="args">'Try' to try all, or any integer to shift.</param>
        public static void Run(List<string> args)
        {
            if (args.Count != 2)
            {
                Help();
                return;
            }

            string arg2 = args[1].ToUpper();

            int shift;

            if (arg2 == "TRY")
            {
                List<string> shifts = new List<string>();
                for (int i = 0; i < 26; i++)
                {
                    shifts.Add(Caesar.Encrypt(CipherData.CipherText, i));
                }

                var result = Frequency.RankByFreqAndBigrams(shifts);

                foreach (var t in result)
                {
                    Console.WriteLine(t.Item1.Take(50).ToArray());
                    Console.WriteLine("Shift: {0}   Freq Score {1}   Bigrams Count {2}\n", 
                                        shifts.IndexOf(t.Item1), t.Item2, t.Item3);
                }

            }
            else if (int.TryParse(args[1].ToUpper(), out shift))
            {
                var encrypted = Caesar.Encrypt(CipherData.CipherText, shift);

                Console.WriteLine(encrypted);
                Console.Write("\nCurrent cipher shifted by {0} places forward.", shift);

                if (Program.AskYesNo(" Save? Y/N"))
                {
                    CipherData.CipherText = encrypted;
                }
            }
            else
            {
                Help();
            }
        }
    }
}
