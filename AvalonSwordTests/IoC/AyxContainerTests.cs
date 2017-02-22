using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.IoC.Tests
{
    [TestClass()]
    public class AyxContainerTests
    {
        [TestMethod()]
        public void WireTest()
        {
            var di = new AyxContainer();
            Assert.AreEqual(di.Count, 0);
            di.Wire<ITest, TestA>();
            Assert.AreEqual(di.Count, 1);
            di.Wire<TestB>();
            Assert.AreEqual(di.Count, 2);

            var a1 = di.Get<ITest>();
            var a2 = di.Get<ITest>();
            Assert.AreNotSame(a1, a2);

            var b1 = di.Get<TestB>();
            var b2 = di.Get<TestB>();
            Assert.AreNotSame(b1, b2);
        }

        [TestMethod()]
        public void WireSingletonTest()
        {
            var di = new AyxContainer();
            var single = new TestA();
            di.WireSingleton(single);
            var actual = di.Get<TestA>();
            Assert.AreSame(single, actual);

            di.WireSingleton<TestB>();
            var b1 = di.Get<TestB>();
            var b2 = di.Get<TestB>();
            Assert.AreSame(b1, b2);

            di.WireSingleton<IComputer, AddComputer>();
            var computer1 = di.Get<IComputer>();
            var computer2 = di.Get<IComputer>();
            Assert.AreSame(computer1, computer2);
        }


        [TestMethod]
        public void TokenTest()
        {
            var di = new AyxContainer();
            di.Wire<ITest, TestA>("a");
            di.Wire<ITest, TestB>("b");

            var a = di.Get<ITest>("a");
            var b = di.Get<ITest>("b");
            var test = di.Get<ITest>();

            Assert.AreEqual("TestA", a.Show());
            Assert.AreEqual("TestB", b.Show());
            Assert.IsNotNull(test);
        }

        [TestMethod]
        public void ConstructureInjectionTest()
        {
            var di = new AyxContainer();
            di.Wire<IComputer, AddComputer>();
            di.WireSingleton<Logger>();

            var test = di.Get<InjectModel>();
            var result = test.Compute(4, 5);
            Assert.AreEqual(9, result);

            var text = "test";
            var expected = "console:" + text;
            var actual = test.Log(text);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void PropertyInjectTest()
        {
            var di = new AyxContainer();
            di.Wire<IComputer, AddComputer>();
            di.WireSingleton<Logger>();

            var test = di.Get<AttributeModel>();
            var result = test.Compute(4, 5);
            Assert.AreEqual(9, result);

            var text = "test";
            var expected = "console:" + text;
            var actual = test.Log(text);
            Assert.AreEqual(expected, actual);
        }
    }

    public interface ITest
    {
        string Show();
    }

    public interface IComputer
    {
        double Compute(double a, double b);
    }

    public class InjectModel
    {
        private IComputer _computer;
        private Logger _logger;

        public InjectModel(IComputer computer, Logger logger)
        {
            _computer = computer;
            _logger = logger;
        }

        public string Log(string text)
        {
            return _logger.ConsoleOut(text);
        }

        public double Compute(double a, double b)
        {
            return _computer.Compute(a, b);
        }
    }

    public class AttributeModel
    {
        [AutoInject]
        public IComputer Computer { get; set; }
        [AutoInject]
        public Logger Logger { get; set; }

        public string Log(string text)
        {
            return Logger.ConsoleOut(text);
        }

        public double Compute(double a, double b)
        {
            return Computer.Compute(a, b);
        }
    }

    public class TestA : ITest
    {
        public string Show()
        {
            return "TestA";
        }
    }

    public class TestB : ITest
    {
        public string Show()
        {
            return "TestB";
        }
    }

    public class AddComputer : IComputer
    {
        public double Compute(double a, double b)
        {
            return a + b;
        }
    }

    public class MinusComputer : IComputer
    {
        public double Compute(double a, double b)
        {
            return a - b;
        }
    }

    public class Logger
    {
        public string ConsoleOut(string content)
        {
            var result = "console:" + content;
            Console.WriteLine(result);
            return result;
        }
    }
}