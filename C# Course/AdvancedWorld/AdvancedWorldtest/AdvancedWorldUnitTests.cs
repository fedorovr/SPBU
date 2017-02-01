using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdvancedWorld;

namespace AdvancedWorldtest
{
    [TestClass]
    public class AdvancedWorldUnitTests
    {
        private const String MaleName = "Иван";
        private const String MalePatronymic = "Петрович";
        private const String FemaleName = "Лолита";
        private const String ChildPatronymic = "Иванович";
        private const int Age = 42;


        [TestMethod]
        [ExpectedException(typeof(ParentsEqualGenderException))]
        public void NoHomoCouplesTest   ()
        {
            var female1 = HumanCreator.GetFemaleHuman();
            var female2 = HumanCreator.GetFemaleHuman();
            HumanMatcher.Couple(female1, female2);
        }


        [TestMethod]
        public void GirlStudentTest()
        {
            var girl = new Girl(FemaleName, Age);
            var student = new Student(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, student);
            }
            while (child == null);
            Assert.IsTrue(child is Girl);
        }

        [TestMethod]
        public void GirlBotanTest()
        {
            var girl = new Girl(FemaleName, Age);
            var botan = new Botan(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, botan);
            }
            while (child == null);
            Assert.IsTrue(child is SmartGirl);
        }

        [TestMethod]
        public void PrettyGirlStudentTest()
        {
            var girl = new PrettyGirl(FemaleName, Age);
            var student = new Student(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, student);
            }
            while (child == null);
            Assert.IsTrue(child is PrettyGirl);
        }

        [TestMethod]
        public void PrettyGirlBotanTest()
        {
            var girl = new PrettyGirl(FemaleName, Age);
            var botan = new Botan(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, botan);
            }
            while (child == null);
            Assert.IsTrue(child is PrettyGirl);
        }

        [TestMethod]
        public void SmartGirlStudentTest()
        {
            var girl = new SmartGirl(FemaleName, Age);
            var student = new Student(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, student);
            }
            while (child == null);
            Assert.IsTrue(child is Girl);
        }

        [TestMethod]
        public void SmartGirlBotanTest()
        {
            var girl = new SmartGirl(FemaleName, Age);
            var botan = new Botan(MaleName, Age, MalePatronymic);
            IHasName child = null;
            do
            {
                child = HumanMatcher.Couple(girl, botan);
            }
            while (child == null);
            Assert.IsTrue(child is Book);
        }
    }
}
