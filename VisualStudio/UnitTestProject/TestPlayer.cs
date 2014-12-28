using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;

namespace UnitTestProject
{
    [TestClass]
    public class TestPlayer
    {
        [TestMethod]
        public void TestCreator()
        {
            Player p = new Player();
            Assert.AreEqual(p.name,"Defaultplayer");
            Assert.AreEqual(p.currentScore, 0);
        }

        [TestMethod]
        public void TestCreator2()
        {
            Player p = new Player("PlayerOne", FactoryPopulation.populationType.Elf);
            Assert.AreEqual(p.name, "PlayerOne");
            Assert.AreEqual(p.currentScore, 0);
            Assert.AreEqual(FactoryPopulation.populationType.Elf, p.populationType);

            int n = p.nbUnitAlive();
            Assert.AreEqual(n, 0);
            //TODO addUnits
        }
    }
}
