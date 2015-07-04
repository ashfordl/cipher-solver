using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Ciphers;

namespace CipherSolverTests
{
    [TestClass]
    public class CaesarTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            Assert.AreEqual("DEFGHIJKLMNOPQRSTUVWXYZABC",
                Caesar.Encrypt("abcdefghijklmnopqrstuvwxyz", 3));

            Assert.AreEqual("EFGHIJKLMNOP  !  QRSTUVWXYZABCD",
                Caesar.Encrypt("abcdefghijkl  !  mnopqrstuvwxyz", 4));
            
            Assert.AreEqual("ABC",
                Caesar.Encrypt("abc", 26));

            Assert.AreEqual("XYZ",
                Caesar.Encrypt("abc", -3));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz",
                Caesar.Decrypt("DEFGHIJKLMNOPQRSTUVWXYZABC", 3));

            Assert.AreEqual("abcdefghijkl  !  mnopqrstuvwxyz",
                Caesar.Decrypt("EFGHIJKLMNOP  !  QRSTUVWXYZABCD", 4));

            Assert.AreEqual("abc",
                Caesar.Decrypt("ABC", 26));

            Assert.AreEqual("abc",
                Caesar.Decrypt("XYZ", -3));
        }
    }
}
