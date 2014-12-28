using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;


namespace UnitTestProject
{
    [TestClass]
    public class TestUnits
    {
        [TestMethod]
        public void TestCreator()
        {
            Unit u = new Unit();
            Assert.IsTrue(u.isAlive);
            Assert.AreEqual(u.healthPoint, 5);
            Assert.AreEqual(u.movementPoint, 1);
            Assert.AreEqual(u.bonusPoint, 0);

            Unit u2 = new UnitElf();
            Assert.IsTrue(u2.isAlive);
            Assert.AreEqual(u2.healthPoint, 5);
            Assert.AreEqual(u2.movementPoint, 1);
            Assert.AreEqual(u2.bonusPoint, 0);
        }

        [TestMethod]
        public void TestDie()
        {
            Unit u = new Unit();
            Assert.IsTrue(u.isAlive);
            u.die();
            Assert.IsFalse(u.isAlive);
        }
    }
}
