using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Mapper.Tests
{
    [TestClass()]
    public class AvalonMapperTests
    {
        [TestMethod()]
        public void CreateMapTest()
        {
            var mapper = new AvalonMapper();
            var info = mapper.CreateMap<TestSrc, TestDest>();

            Assert.AreEqual(1, mapper.Cache.Count);

            var test = mapper.Map<TestDest>(new TestSrc());
            Assert.IsNotNull(test);
            Assert.AreEqual(1234, test.IntTest);
            Assert.AreEqual("strTest", test.StringTest);
            Assert.AreEqual("2017-02-23 12:12:12", test.DateTimeTest.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(null, test.TestAA);
            Assert.AreEqual(0, test.TestB);
            Assert.AreEqual(null, test.TestCC);
        }

        [TestMethod()]
        public void MapTest()
        {
            //map without CreateMap
            var mapper = new AvalonMapper();
            Assert.AreEqual(0, mapper.Cache.Count);

            var test = mapper.Map<TestDest>(new TestSrc());
            Assert.IsNotNull(test);
            Assert.AreEqual(1234, test.IntTest);
            Assert.AreEqual("strTest", test.StringTest);
            Assert.AreEqual("2017-02-23 12:12:12", test.DateTimeTest.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(null, test.TestAA);
            Assert.AreEqual(0, test.TestB);
            Assert.AreEqual(null, test.TestCC);

            Assert.AreEqual(1, mapper.Cache.Count);
        }

        [TestMethod()]
        public void MapWithNameMappingTest()
        {
            var mapper = new AvalonMapper();
            var info = mapper.CreateMap<TestSrc, TestDest>(
                new Dictionary<string, string>
                {
                    {"TestA","TestAA" },
                    {"TestC","TestCC" },
                });

            Assert.AreEqual(1, mapper.Cache.Count);

            var test = mapper.Map<TestDest>(new TestSrc());
            Assert.IsNotNull(test);
            Assert.AreEqual(1234, test.IntTest);
            Assert.AreEqual("strTest", test.StringTest);
            Assert.AreEqual("2017-02-23 12:12:12", test.DateTimeTest.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual("TestA", test.TestAA);
            Assert.AreEqual(0, test.TestB);
            Assert.AreEqual("TestC", test.TestCC);
        }
    }

    public class TestSrc
    {
        public int IntTest { get; set; } = 1234;
        public string StringTest { get; set; } = "strTest";
        public DateTime DateTimeTest { get; set; } = DateTime.Parse("2017-02-23 12:12:12");
        public string TestA { get; set; } = "TestA";
        public string TestB { get; set; } = "TestB";
        public string TestC { get; set; } = "TestC";
    }

    public class TestDest
    {
        public int IntTest { get; set; }
        public string StringTest { get; set; }
        public DateTime DateTimeTest { get; set; }
        public string TestAA { get; set; }
        public int TestB { get; set; }
        public string TestCC { get; set; }
    }
}