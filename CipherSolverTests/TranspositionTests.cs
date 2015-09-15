using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolverLibrary.Ciphers;

namespace CipherSolverTests
{
    [TestClass]
    public class TranspositionTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            Assert.AreEqual("EVLNEACDTKESEAQROFOJDEECUWIREE",
                Transposition.Encrypt("WEAREDISCOVEREDFLEEATONCEQKJEU", "zebras"));

            Assert.AreEqual("EVLNACDTESEAROFODEECWIREE",
                Transposition.Encrypt("WEAREDISCOVEREDFLEEATONCE", "zebras"));

            Assert.AreEqual("CAEENSOIAEDRLEFWEDREEVTOC",
                Transposition.Encrypt("EVLNACDTESEAROFODEECWIREE", "stripe"));

            Assert.AreEqual("ROFOACDTEDSEEEACWEIVRLENE",
                Transposition.Encrypt("WEAREDISCOVEREDFLEEATONCE", "tomato", false));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual("WEAREDISCOVEREDFLEEATONCEQKJEU",
                Transposition.Encrypt("EVLNEACDTKESEAQROFOJDEECUWIREE", "zebras"));

            Assert.AreEqual("WEAREDISCOVEREDFLEEATONCE",
                Transposition.Encrypt("EVLNACDTESEAROFODEECWIREE", "zebras"));

            Assert.AreEqual("EVLNACDTESEAROFODEECWIREE",
                Transposition.Encrypt("CAEENSOIAEDRLEFWEDREEVTOC", "stripe"));

            Assert.AreEqual("WEAREDISCOVEREDFLEEATONCE",
                Transposition.Encrypt("ROFOACDTEDSEEEACWEIVRLENE", "tomato", false));
        }
    }
}
