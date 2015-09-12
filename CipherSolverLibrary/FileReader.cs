using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CipherSolver
{
    class FileReader
    {
        public static Dictionary<char, double> ReadLetterFrequencies()
        {
            string file = File.ReadAllText("letter_frequencies.json");

            Dictionary<char, double> freq = JsonConvert.DeserializeObject<Dictionary<char, double>>(file);

            return freq;
        }

        public static List<string> ReadBigrams()
        {
            string file = File.ReadAllText("bigrams.json");

            List<string> bigrams = JsonConvert.DeserializeObject<List<string>>(file);

            return bigrams;
        }
    }
}
