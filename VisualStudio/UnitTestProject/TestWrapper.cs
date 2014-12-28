using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wrapper;


namespace UnitTestProject
{
    [TestClass]
    public class TestWrapper
    {
        [TestMethod]
        public void TestComputeFoo()
        {
            WrapperAlgo algo = new WrapperAlgo();
            Assert.AreEqual(1, algo.computeFoo());
        }
    }
}
