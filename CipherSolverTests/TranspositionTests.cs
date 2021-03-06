﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Ciphers;

namespace CipherSolverTests
{
    [TestClass]
    public class TranspositionTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            Assert.AreEqual("EVLNEACDTKESEAQROFOJDEECUWIREE",
                Transposition.Encrypt("wearediscoveredfleeatonceqkjeu", "zebras"));

            Assert.AreEqual("EVLNACDTESEAROFODEECWIREE",
                Transposition.Encrypt("wearediscoveredfleeatonce", "zebras"));

            Assert.AreEqual("CAEENSOIAEDRLEFWEDREEVTOC",
                Transposition.Encrypt("evlnacdtesearofodeecwiree", "stripe"));

            Assert.AreEqual("ROFOACDTEDSEEEACWEIVRLENE",
                Transposition.Encrypt("wearediscoveredfleeatonce", "tomato", false));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual("wearediscoveredfleeatonceqkjeu",
                Transposition.Decrypt("EVLNEACDTKESEAQROFOJDEECUWIREE", "zebras"));

            Assert.AreEqual("wearediscoveredfleeatonce",
                Transposition.Decrypt("EVLNACDTESEAROFODEECWIREE", "zebras"));

            Assert.AreEqual("evlnacdtesearofodeecwiree",
                Transposition.Decrypt("CAEENSOIAEDRLEFWEDREEVTOC", "stripe"));

            Assert.AreEqual("wearediscoveredfleeatonce",
                Transposition.Decrypt("ROFOACDTEDSEEEACWEIVRLENE", "tomato", false));
        }
    }
}
