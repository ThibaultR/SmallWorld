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
            Assert.IsNull(ft.plain);
            Plain p = ft.getOrCreatePlain();
            Assert.IsNotNull(ft.plain);
            Assert.AreEqual(ft.plain, p);
            Plain p2 = ft.getOrCreatePlain();
            Assert.AreEqual(p2, p);

            Assert.IsNull(ft.desert);
            Desert d = ft.getOrCreateDesert();
            Assert.AreEqual(ft.desert, d);

            Assert.IsNull(ft.mountain);
            Mountain m = ft.getOrCreateMountain();
            Assert.AreEqual(ft.mountain, m);

            Assert.IsNull(ft.forest);
            Forest f = ft.getOrCreateForest();
            Assert.AreEqual(ft.forest, f);

            Assert.AreNotEqual(p, d);
        }

    }
}

