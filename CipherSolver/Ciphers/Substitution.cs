using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CipherSolver.Analysis;

namespace CipherSolver.Ciphers
{
    public static class Substitution
    {
        public static string AttemptSubstition(string cipher, int chars = 5)
        {
            cipher = cipher.ToUpper();

            var naturalTop = Frequency.NATURAL_FREQUENCIES.OrderByDescending(key => key.Value);

            var resultsTop = Frequency.Analyse(cipher).OrderByDescending(key => key.Value);

            for (int i = 0; i <= chars; i++)
            {
                cipher = cipher.Replace(resultsTop.ElementAt(i).Key, naturalTop.ElementAt(i).Key);
            }

            return cipher;
        }
    }
}
