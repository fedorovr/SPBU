using Drom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PalindromeTests
{
    [TestClass]
    public class PalindromeTests
    {
        [TestMethod]
        public void SimpleOddPalindrome()
        {
            Assert.IsTrue(PalindromeChecker.IsPalindrome("ABA"));
        }

        [TestMethod]
        public void SimpleEvenPalindrome()
        {
            Assert.IsTrue(PalindromeChecker.IsPalindrome("ABBA"));
        }

        [TestMethod]
        public void NotPalindrome()
        {
            Assert.IsFalse(PalindromeChecker.IsPalindrome("ABBAT"));
        }

        [TestMethod]
        public void NotPalindromeWithDelimeters()
        {
            Assert.IsFalse(PalindromeChecker.IsPalindrome("AA . bc . AA"));
        }

        [TestMethod]
        public void OddPalindromeWithDelimeters()
        {
            Assert.IsTrue(PalindromeChecker.IsPalindrome("AA . b AA"));
        }

        [TestMethod]
        public void EvenPalindromeWithDelimeters()
        {
            Assert.IsTrue(PalindromeChecker.IsPalindrome("AA . b ! b AA"));
        }
    }
}
