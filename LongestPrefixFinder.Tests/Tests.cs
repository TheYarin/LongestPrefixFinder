using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LongestPrefixFinder.Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void BasicTest()
        {
            LongestPrefixFinder<string> finder = new LongestPrefixFinder<string>();

            finder.AddPrefix("123", "wow");
            finder.AddPrefix("124", "wuw");
            finder.AddPrefix("12", "wax");
            finder.AddPrefix("1234", "wow1");
            finder.AddPrefix("1234", "wow2");

            Assert.AreEqual("wow2", finder.Find("123456"));
            Assert.AreEqual("wax", finder.Find("12"));
            Assert.AreEqual(null, finder.Find(""));

            try
            {
                Assert.AreEqual(null, finder.Find(null));
                Assert.Fail();
            }
            catch (ArgumentException) { }

            finder.AddPrefix("", "Hello");

            try
            {
                finder.AddPrefix(null, "Hello 2, world!");
                Assert.Fail();
            }
            catch (ArgumentNullException) { }
        }
    }
}
