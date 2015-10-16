using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Ciphers;

namespace CipherSolverTests
{
    /// <summary>
    /// Summary description for PolyalphabeticTests
    /// </summary>
    [TestClass]
    public class PolyalphabeticTests
    {
        [TestMethod]
        public void Encrypt()
        {
            Assert.AreEqual(Polyalphabetic.Encrypt("attackatdawn", "lemon"),
                "LXFOPVEFRNHR");
        }

        [TestMethod]
        public void Decrypt()
        {
            Assert.AreEqual(Polyalphabetic.Decrypt("LXFOPVEFRNHR", "lemon"),
                "attackatdawn");
        }
    }
}
