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
            Assert.AreEqual("Defaultplayer", p.name);
            Assert.AreEqual(0, p.currentScore);
        }

        [TestMethod]
        public void TestCreator2()
        {
            Player p = new Player("PlayerOne", FactoryPopulation.populationType.Elf);
            Assert.AreEqual("PlayerOne", p.name);
            Assert.AreEqual(0, p.currentScore);
            Assert.AreEqual(FactoryPopulation.populationType.Elf, p.populationType);

            int n = p.nbUnitAlive();
            Assert.AreEqual(0, n);
        }
    }
}
