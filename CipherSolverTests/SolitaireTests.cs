using Microsoft.VisualStudio.TestTools.UnitTesting;
using CipherSolver.Ciphers.Solitaire;

namespace CipherSolverTests
{
    [TestClass]
    public class SolitaireTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            Deck d = new Deck();
            Assert.AreEqual("EXKYIZSGEH", Solitaire.Encrypt("AAAAAAAAAA", d));

            d = new Deck("foo", false);
            Assert.AreEqual("ITHZUJIWGRFARMW", Solitaire.Encrypt("AAAAAAAAAAAAAAA", d));

            d = new Deck("CRYPTONOMICON", false);
            Assert.AreEqual("KIRAKSFJAN", Solitaire.Encrypt("SOLITAIREX", d));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Deck d = new Deck();
            Assert.AreEqual("AAAAAAAAAA", Solitaire.Decrypt("EXKYIZSGEH", d));

            d = new Deck("foo", false);
            Assert.AreEqual("AAAAAAAAAAAAAAA", Solitaire.Decrypt("ITHZUJIWGRFARMW", d));

            d = new Deck("CRYPTONOMICON", false);
            Assert.AreEqual("SOLITAIREX", Solitaire.Decrypt("KIRAKSFJAN", d));
        }
    }
}
