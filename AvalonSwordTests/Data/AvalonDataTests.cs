using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data.Tests
{
    [TestClass()]
    public class AvalonDataTests
    {
        [TestMethod()]
        public void SelectTest()
        {
            var data = new AvalonData(null);
            var test = data.Select<TestData>().GetSQL();
            Assert.AreEqual("SELECT * FROM TestData",test);

            var test2 = data.Select<TestData>().Where("IntProperty>@IntProperty AND StringProperty!=@StringProperty").GetSQL();
            var expected2 = @"SELECT * FROM TestData WHERE IntProperty>@IntProperty AND StringProperty!=@StringProperty";
            Assert.AreEqual(expected2, test2);

            var test3 = data.Select("ID,IntProperty,StringProperty AS Name").From("TestTable").Where("ID=@ID").GetSQL();
            var expected3 = @"SELECT ID,IntProperty,StringProperty AS Name FROM TestTable WHERE ID=@ID";
            Assert.AreEqual(expected3, test3);

            var test4 = data.Select<TestData>().Top(100).Where("IntProperty>@Intporperty").GetSQL();
            var expected4 = @"SELECT TOP 100 * FROM TestData WHERE IntProperty>@Intporperty";
            Assert.AreEqual(expected4, test4);

            var test5 = data.Select<TestData>()
                .JoinOn("TableOne", "IntProperty")
                .JoinOn("TableTwo", "StringProperty=Name")
                .Where("ID=@ID")
                .GetSQL();
            var expected5 = @"SELECT * FROM TestData JOIN TableOne ON TestData.IntProperty=TableOne.IntProperty JOIN TableTwo ON TestData.StringProperty=TableTwo.Name WHERE ID=@ID";
            Assert.AreEqual(expected5, test5);

            var test6 = data.Select("ID,IntProperty,StringProperty AS Name").From("TestTable").Where("ID=@ID").GetSQL();
            Assert.AreSame(test3, test6);

            Assert.AreEqual(5, SqlGenerator.SqlCache.Count);
        }
    }

    class TestData
    {
        public string ID { get; set; }
        public int IntProperty { get; set; }
        public string StringProperty { get; set; }
        public DateTime DateTimeProperty { get; set; }
    }
}