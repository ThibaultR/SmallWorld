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
            Assert.AreEqual(5, u.healthPoint);
            Assert.AreEqual(1, u.movementPoint);
            Assert.AreEqual(0, u.bonusPoint);

            Unit u2 = new UnitElf();
            Assert.IsTrue(u2.isAlive);
            Assert.AreEqual(5, u2.healthPoint);
            Assert.AreEqual(1, u2.movementPoint);
            Assert.AreEqual(0, u2.bonusPoint);
        }

        [TestMethod]
        public void TestDie()
        {
            Unit u = new Unit();
            Assert.IsTrue(u.isAlive);
            u.die();
            Assert.IsFalse(u.isAlive);
        }

        [TestMethod]
        public void TestFactory()
        {
            FactoryPopulation fp = new FactoryPopulation();
            Unit u = fp.createUnit(FactoryPopulation.populationType.Dwarf);
            Assert.IsTrue(u.isAlive);
            Assert.AreEqual(5, u.healthPoint);
            Assert.AreEqual(1, u.movementPoint);
            Assert.AreEqual(0, u.bonusPoint);
            
        }

        [TestMethod]
        public void TestAttack()
        {
            FactoryPopulation fp = new FactoryPopulation();
            Unit u = fp.createUnit(FactoryPopulation.populationType.Dwarf);
            Unit enemy = fp.createUnit(FactoryPopulation.populationType.Orc);
            u.attack(enemy);
            

        }

        [TestMethod]
        public void TestMove()
        {
            FactoryPopulation fp = new FactoryPopulation();
            Unit u =  fp.createUnit(FactoryPopulation.populationType.Dwarf);
            u.move(new Coordinate(3, 2), typeof(Plain));
            Coordinate c = new Coordinate(3, 2);
            Assert.IsTrue(c.Equals(u.coordinate));
            Assert.AreEqual(0.5, u.movementPoint);

            Unit u2 = fp.createUnit(FactoryPopulation.populationType.Dwarf);
            u2.move(new Coordinate(3, 2), typeof(Forest));
            Assert.AreEqual(0, u2.movementPoint);

        }
    }
}
