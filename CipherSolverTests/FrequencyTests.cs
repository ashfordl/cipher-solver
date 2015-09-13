using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Analysis;

namespace CipherSolverTests
{
    [TestClass]
    public class FrequencyTests
    {
        [TestMethod]
        public void FrequencyTest()
        {
            var dict = new Dictionary<char, int>() 
                {
                    { 'G', 0 }, { 'J', 0 }, { 'K', 0 }, { 'M', 0 }, { 'P', 0 }, { 'Q', 0 },
                    { 'U', 0 }, { 'X', 0 }, { 'Y', 0 }, { 'Z', 0 },

                    { 'B', 1 }, { 'F', 1 }, { 'H', 1 }, { 'I', 1 }, { 'L', 1 }, { 'R', 1 },
                    { 'S', 1 }, { 'T', 1 }, { 'W', 1 },

                    { 'A', 2 }, { 'C', 2 }, { 'D', 2 }, { 'N', 2 }, { 'O', 2 }, { 'V', 2 },

                    { 'E', 9 }
                };

            // Test with only letters/spaces
            var result = Frequency.Analyse("we have been discovered flee at once");
            Assert.IsTrue(dict.All(e => result.Contains(e)));

            // Test with capitals and other punctuation
            result = Frequency.Analyse("We haVe been; DiscoverEd flee at oNce.12");
            Assert.IsTrue(dict.All(e => result.Contains(e)));
        }
    
        [TestMethod]
        public void CountBigramsTest()
        {
            string text = "the quick brown fox jumped over the lazy dog";
            Assert.AreEqual(7, Frequency.CountBigrams(text));

            text = "tH e quickbr\"ownfox jumped overThe lazy dog!";
            Assert.AreEqual(7, Frequency.CountBigrams(text));

            text = "we have been discovered flee at once";
            Assert.AreEqual(14, Frequency.CountBigrams(text));

            text = "We haVe been; DiscoverEd flee at oNce.12";
            Assert.AreEqual(14, Frequency.CountBigrams(text));
        }
    }
}
