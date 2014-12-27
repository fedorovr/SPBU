using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Geometry5;

namespace GeometryTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void SimpleInsideTest1()
        {
            int testResShouldBe = 1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(0, 0),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(100, 100),
                            new Tuple<int, int>(-100, 100),
                            new Tuple<int, int>(0, -100)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleInsideTest2()
        {
            int testResShouldBe = 1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(1, 1),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(3, 0),
                            new Tuple<int, int>(0, 3),
                            new Tuple<int, int>(0, 0)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleInsideTest3()
        {
            int testResShouldBe = 1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(-8, -8),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(-10, -10),
                            new Tuple<int, int>(-5, -10),
                            new Tuple<int, int>(-10, -5)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOutsideTest1()
        {
            int testResShouldBe = -1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(2, 2),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(3, 0),
                            new Tuple<int, int>(0, 3),
                            new Tuple<int, int>(0, 0)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOutsideTest2()
        {
            int testResShouldBe = -1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(51, 0),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(100, 100),
                            new Tuple<int, int>(-100, 100),
                            new Tuple<int, int>(0, -100)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOutsideTest3()
        {
            int testResShouldBe = -1;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(-7, -7),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(-10, -10),
                            new Tuple<int, int>(-5, -10),
                            new Tuple<int, int>(-10, -5)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOnLineTest1()
        {
            int testResShouldBe = 0;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(2, 1),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(3, 0),
                            new Tuple<int, int>(0, 3),
                            new Tuple<int, int>(0, 0)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOnLineTest2()
        {
            int testResShouldBe = 0;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(50, 0),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(100, 100),
                            new Tuple<int, int>(-100, 100),
                            new Tuple<int, int>(0, -100)));
            Assert.AreEqual(testResShouldBe, res);
        }

        [TestMethod]
        public void SimpleOnLineTest3()
        {
            int testResShouldBe = 0;
            int res = Geometry5.GeometrySolver.checkIsAPointInsideOfATriangle(
                        new Tuple<int, int>(1, 2),
                        new Tuple<Tuple<int, int>, Tuple<int, int>, Tuple<int, int>>(
                            new Tuple<int, int>(3, 0),
                            new Tuple<int, int>(0, 3),
                            new Tuple<int, int>(0, 0)));
            Assert.AreEqual(testResShouldBe, res);
        }
    }
}
