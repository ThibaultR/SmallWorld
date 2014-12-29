using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UML_SW;


namespace UnitTestProject
{
    [TestClass]
    public class TestFactoryTile
    {
        [TestMethod]
        public void TestFactory()
        {
            FactoryTile ft = new FactoryTile();
            Assert.AreEqual(ft.plain, null);
            Plain p = ft.getOrCreatePlain();
            Assert.AreNotEqual(ft.plain, null);
            Assert.AreEqual(ft.plain, p);
            Plain p2 = ft.getOrCreatePlain();
            Assert.AreEqual(p2, p);

            Assert.AreEqual(ft.desert, null);
            Desert d = ft.getOrCreateDesert();
            Assert.AreEqual(ft.desert, d);

            Assert.AreEqual(ft.mountain, null);
            Mountain m = ft.getOrCreateMountain();
            Assert.AreEqual(ft.mountain, m);

            Assert.AreEqual(ft.forest, null);
            Forest f = ft.getOrCreateForest();
            Assert.AreEqual(ft.forest, f);

            Assert.AreNotEqual(p, d);
        }

    }
}

