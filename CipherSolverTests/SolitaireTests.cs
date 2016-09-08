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
            Assert.AreEqual("EXKYIZSGEH", Solitaire.Encrypt("AAAAAAAAAA", ref d));

            d = new Deck("foo", false);
            Assert.AreEqual("ITHZUJIWGRFARMW", Solitaire.Encrypt("AAAAAAAAAAAAAAA", ref d));

            d = new Deck("CRYPTONOMICON", false);
            Assert.AreEqual("KIRAKSFJAN", Solitaire.Encrypt("SOLITAIREX", ref d));
        }

        [TestMethod]
        public void DecryptTest()
        {
            Deck d = new Deck();
            Assert.AreEqual("aaaaaaaaaa", Solitaire.Decrypt("EXKYIZSGEH", ref d));

            d = new Deck("foo", false);
            Assert.AreEqual("aaaaaaaaaaaaaaa", Solitaire.Decrypt("ITHZUJIWGRFARMW", ref d));

            d = new Deck("CRYPTONOMICON", false);
            Assert.AreEqual("solitairex", Solitaire.Decrypt("KIRAKSFJAN", ref d));
        }
    }
}
