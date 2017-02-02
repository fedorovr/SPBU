using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexValidator;

namespace RegexTests
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void RuZipCodeTest()
        {
            Assert.IsTrue(Validator.Validate("198000").Equals(Validator.ResultZip));
        }

        [TestMethod]
        public void UsZipCodeTest()
        {
            Assert.IsTrue(Validator.Validate("12345").Equals(Validator.ResultZip));
        }

        [TestMethod]
        public void LongUsZipCodeTest()
        {
            Assert.IsTrue(Validator.Validate("12345-1122").Equals(Validator.ResultZip));
        }

        [TestMethod]
        public void LongUsZipCodeTestFail()
        {
            Assert.IsTrue(Validator.Validate("12345-11222").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void PhoneTestPlus7()
        {
            Assert.IsTrue(Validator.Validate("+7(905)123-45-67").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTest8()
        {
            Assert.IsTrue(Validator.Validate("8(905)123-45-67").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTestNoCountryCode()
        {
            Assert.IsTrue(Validator.Validate("(812)123-45-67").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTestOneDash()
        {
            Assert.IsTrue(Validator.Validate("8(812)123-4567").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTestNoDashes()
        {
            Assert.IsTrue(Validator.Validate("8(812)1234567").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTestNoBracketsInCityCode()
        {
            Assert.IsTrue(Validator.Validate("8121234567").Equals(Validator.ResultPhone));
        }

        [TestMethod]
        public void PhoneTestTooManyDigits()
        {
            Assert.IsTrue(Validator.Validate("(812)12345678").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void PhoneTestIllegalCountryCode()
        {
            Assert.IsTrue(Validator.Validate("+1(905)123-45-67").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestOnlyDigits()
        {
            Assert.IsTrue(Validator.Validate("1@1").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestNoAt()
        {
            Assert.IsTrue(Validator.Validate("sadDDsa_example.com").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestOnlyDomain()
        {
            Assert.IsTrue(Validator.Validate("google.com").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestShortNickAndDomain()
        {
            Assert.IsTrue(Validator.Validate("a@b.cc").Equals(Validator.ResultEmail));
        }

        [TestMethod]
        public void EmailTestUsualDomain()
        {
            Assert.IsTrue(Validator.Validate("my@domain.info").Equals(Validator.ResultEmail));
        }
        
        [TestMethod]
        public void EmailTestHermitage()
        {
            Assert.IsTrue(Validator.Validate("paints_department@hermitage.museum").Equals(Validator.ResultEmail));
        }

        [TestMethod]
        public void EmailTestLotsOfSubdomains()
        {
            Assert.IsTrue(Validator.Validate("yo@domain.somedomain.onemore.andonemoredomain.indomainzone").Equals(Validator.ResultEmail));
        }

        [TestMethod]
        public void EmailTestIncorrectDomainZone()
        {
            Assert.IsTrue(Validator.Validate("a@b.c").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestTwoDotsInARowInNickName()
        {
            Assert.IsTrue(Validator.Validate("a..b@mail.ru").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestNickStartsWithDot()
        {
            Assert.IsTrue(Validator.Validate(".abcde@mail.ru").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestNickStartsWithDigit()
        {
            Assert.IsTrue(Validator.Validate("1abcde@mail.ru").Equals(Validator.ResultInvalid));
        }

        [TestMethod]
        public void EmailTestContainsSpace()
        {
            Assert.IsTrue(Validator.Validate("ab cde@mail.ru").Equals(Validator.ResultInvalid));
        }
    }
}
