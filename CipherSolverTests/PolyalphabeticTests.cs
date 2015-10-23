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
        public void EncryptTest()
        {
            Assert.AreEqual(Polyalphabetic.Encrypt("attackatdawn", "lemon"),
                "LXFOPVEFRNHR");    
        }

        [TestMethod]
        public void DecryptTest()
        {
            Assert.AreEqual(Polyalphabetic.Decrypt("LXFOPVEFRNHR", "lemon"),
                "attackatdawn");
        }
    }
}
