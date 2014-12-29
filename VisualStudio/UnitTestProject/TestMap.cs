using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;

namespace UnitTestProject
{
    [TestClass]
    public class TestMap
    {
        [TestMethod]
        public void TestMapCreation()
        {
            Map m = new Map();
            Assert.AreNotEqual(m.tilesList, null);
            Assert.AreEqual(m.tilesList.Count, 0);

            m.setStrategy(Map.mapType.SMALL);
            Assert.AreEqual(m.strategy.size, 10);

            m.createMap();
            Assert.AreEqual(m.tilesList.Count, 100);

            int cpt = 0;
            for (int i = 0; i < 100; i++) {
                if (m.tilesList[i].GetType() == typeof(Plain)) {
                    cpt++;
                }
            }
            Assert.AreEqual(cpt, 25);
        }
    }
}
