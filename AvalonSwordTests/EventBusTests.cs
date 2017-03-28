using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Tests
{
    [TestClass()]
    public class EventBusTests
    {
        [TestMethod()]
        public void OnTest()
        {
            var p = "asdf";
            var q = "ffff";
            var bus = new EventBus();
            bus.On("test", o => { p = o.ToString(); });
            bus.On(Events.TestTwo, o => { q = o.ToString(); });

            bus.Emit("test", "foo");
            Assert.AreEqual("foo", p);

            bus[Events.TestTwo]("bar");
            Assert.AreEqual("bar", q);
        }
    }

    public enum Events
    {
        TestOne,
        TestTwo,
    }
}