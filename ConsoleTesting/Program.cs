using System;
using System.Collections.Generic;
using System.Linq;
using CipherSolver;
using CipherSolver.Ciphers;
using CipherSolver.Analysis;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string plain = "a";

            List<string> lolo = new List<string>();
            for (int i = 0; i < 26; i++)
            {
                lolo.Add(Caesar.Encrypt(plain, i));
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var result = Frequency.RankByFreqAndBigrams(lolo)
                                    .Select(t => new String(t.Item1.Take(8).ToArray())
                                                + "   " + t.Item2.ToString()
                                                + "   " + t.Item3.ToString());
            watch.Stop();

            Console.WriteLine(watch.ElapsedMilliseconds);

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }

            var top10freqs = Frequency.NATURAL_FREQUENCIES
                                        .OrderByDescending(k => k.Value)
                                        .Select(kvp => kvp.Key.ToUpper())
                                        .Take(10)
                                        .ToArray();
            Console.WriteLine(top10freqs);


            /*var top10freqs = Frequency.NATURAL_FREQUENCIES
                                        .OrderByDescending(k => k.Value)
                                        .Select(kvp => kvp.Key.ToUpper())
                                        .Take(10)
                                        .ToArray();

            Console.WriteLine(top10freqs);

            for (int i = 0; i < 26; i++)
            {
                string encrypted = Caesar.Encrypt(plain, i);

                Console.Write(i);
                Console.Write("   ");
                Console.Write(Frequency.CountBigrams(encrypted));

                var freqs = Frequency.Analyse(encrypted)
                                    .OrderByDescending(k => k.Value)
                                    .Select(kvp => kvp.Key)
                                    .Take(10)
                                    .Where(letter => top10freqs.Contains(letter))
                                    .Count();


                Console.Write("   ");
                Console.WriteLine(freqs);
            }*/

            Console.ReadKey();
        }
    }
}
