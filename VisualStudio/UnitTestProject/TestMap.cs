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
            Assert.IsNotNull(m.tilesList);
            Assert.AreEqual(0, m.tilesList.Count);

            m.setStrategy(Map.mapType.SMALL);
            Assert.AreEqual(10, m.strategy.size);

            m.createMap();
            Assert.AreEqual(100, m.tilesList.Count);

            int cpt = 0;
            for (int i = 0; i < 100; i++) {
                if (m.tilesList[i].GetType() == typeof(Plain)) {
                    cpt++;
                }
            }
            Assert.AreEqual(25, cpt);
        }
    }
}
