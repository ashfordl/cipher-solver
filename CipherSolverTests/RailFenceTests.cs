using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Ciphers;

namespace CipherSolverTests
{
    [TestClass]
    public class RailFenceTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            Assert.AreEqual("wecrlteerdsoeefeaocaivden", 
                RailFence.Encrypt("wearediscoveredfleeatonce", 3));

            Assert.AreEqual("tlcimntphifeirsroasiiiheaecpeaofrpsocernhftonr", 
                RailFence.Encrypt("therailfencecipherisaformoftranspositioncipher", 4));

            Assert.AreEqual("abc",
                RailFence.Encrypt("abc", 3));

            Assert.AreEqual("aebdc",
                RailFence.Encrypt("abcde", 3));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual("wearediscoveredfleeatonce",
                RailFence.Decrypt("wecrlteerdsoeefeaocaivden", 3));

            Assert.AreEqual("therailfencecipherisaformoftranspositioncipher", 
                RailFence.Decrypt("tlcimntphifeirsroasiiiheaecpeaofrpsocernhftonr", 4));

            Assert.AreEqual("abc",
                RailFence.Decrypt("abc", 3));

            Assert.AreEqual("abcde",
                RailFence.Decrypt("aebdc", 3));
        }
    }
}
