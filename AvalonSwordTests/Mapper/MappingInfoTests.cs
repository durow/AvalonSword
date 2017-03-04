using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fuck.Foo.Bar;
using System.Diagnostics;

namespace Ayx.AvalonSword.Mapper.Tests
{
    [TestClass()]
    public class MappingInfoTests
    {
        [TestMethod()]
        public void AddFilterTest()
        {
            var mapper = new AvalonMapper();
            var src1 = new TestSrc();
            var src2 = new TestSrc
            {
                IntTest = 10,
                StringTest = "Fool"
            };

            mapper.CreateMap<TestSrc, TestDest>()
                .AddFilter<TestSrc>("IntTest", s => s.IntTest < 100)
                .AddFilter<TestSrc>("StringTest", s=>s.StringTest != "Fool");

            var test1 = mapper.Map<TestDest>(src1);
            var test2 = mapper.Map<TestDest>(src2);

            Assert.AreEqual(0, test1.IntTest);
            Assert.AreEqual("strTest", test1.StringTest);

            Assert.AreEqual(10, test2.IntTest);
            Assert.IsTrue(string.IsNullOrEmpty(test2.StringTest));
        }

        [TestMethod]
        public void ReflectionTest()
        {
            var logger = new TestLogger();
            Console.WriteLine(logger.NameSpace);
        }
    }
}

namespace Fuck.Foo.Bar
{
    public class TestLogger
    {
        public string NameSpace { get; set; }
        public string MethodName { get; set; }

        public TestLogger()
        {
            var ss = new StackTrace(true);
            var m = ss.GetFrame(1).GetMethod();
            NameSpace = m.DeclaringType.FullName;
            MethodName = m.Name;
        }
    }
}