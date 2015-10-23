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
            Assert.AreEqual("WECRLTEERDSOEEFEAOCAIVDEN", 
                RailFence.Encrypt("wearediscoveredfleeatonce", 3));

            Assert.AreEqual("TLCIMNTPHIFEIRSROASIIIHEAECPEAOFRPSOCERNHFTONR", 
                RailFence.Encrypt("therailfencecipherisaformoftranspositioncipher", 4));

            Assert.AreEqual("ABC",
                RailFence.Encrypt("abc", 3));

            Assert.AreEqual("AEBDC",
                RailFence.Encrypt("abcde", 3));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual("wearediscoveredfleeatonce",
                RailFence.Decrypt("WECRLTEERDSOEEFEAOCAIVDEN", 3));

            Assert.AreEqual("therailfencecipherisaformoftranspositioncipher",
                RailFence.Decrypt("TLCIMNTPHIFEIRSROASIIIHEAECPEAOFRPSOCERNHFTONR", 4));

            Assert.AreEqual("abc",
                RailFence.Decrypt("ABC", 3));

            Assert.AreEqual("abcde",
                RailFence.Decrypt("AEBDC", 3));
        }
    }
}
