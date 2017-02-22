using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ayx.AvalonSword;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Tests
{
    [TestClass()]
    public class UserAuthenticationTests
    {
        [TestMethod()]
        public void CheckGroupTest()
        {
            var ua = new UserAuthentication<TestUser>();
            ua.Groups.Add("GroupA");
            ua.Groups.Add("GroupZ");

            Assert.IsTrue(ua.CheckGroup("GroupA"));
            Assert.IsTrue(ua.CheckGroup("GroupZ"));
            Assert.IsFalse(ua.CheckGroup("GroupF"));
        }

        [TestMethod()]
        public void CheckInGroupsTest()
        {
            var ua = new UserAuthentication<TestUser>();
            ua.Groups.Add("GroupA");
            ua.Groups.Add("GroupB");
            ua.Groups.Add("GroupC");
            ua.Groups.Add("GroupD");
            ua.Groups.Add("GroupE");

            var test = ua.CheckInGroups(
                new List<string>
                {
                    "GroupA",
                    "GroupD",
                    "GroupF",
                });

            Assert.AreEqual(test.Count(), 2);
            Assert.IsTrue(test.Contains("GroupA"));
            Assert.IsTrue(test.Contains("GroupD"));
            Assert.IsFalse(test.Contains("GroupF"));
        }

        [TestMethod()]
        public void CheckRoleTest()
        {
            var ua = new UserAuthentication<TestUser>();
            ua.Roles.Add("RoleA");
            ua.Roles.Add("RoleB");
            ua.Roles.Add("RoleC");

            Assert.IsTrue(ua.CheckRole("RoleA"));
            Assert.IsTrue(ua.CheckRole("RoleC"));
            Assert.IsFalse(ua.CheckRole("RoleF"));
        }

        [TestMethod()]
        public void CheckInRolesTest()
        {
            var ua = new UserAuthentication<TestUser>();
            ua.Roles.Add("RoleA");
            ua.Roles.Add("RoleB");
            ua.Roles.Add("RoleC");
            ua.Roles.Add("RoleD");
            ua.Roles.Add("RoleE");

            var test = ua.CheckInRoles(new List<string>
            {
                "RoleB",
                "RoleE",
                "RoleF",
            });

            Assert.AreEqual(test.Count(), 2);
            Assert.IsTrue(test.Contains("RoleE"));
            Assert.IsFalse(ua.CheckRole("RoleF"));
        }

        [TestMethod()]
        public void CheckRingTest()
        {
            var ua = new UserAuthentication<TestUser>()
            {
                Ring = 5,
            };

            Assert.IsTrue(ua.CheckRing(7));
            Assert.IsFalse(ua.CheckRing(3));
        }

        [TestMethod()]
        public void CheckTokenTest()
        {
            var ua = new UserAuthentication<TestUser>()
            {
                Token = "FDSDF",
            };
            Assert.IsTrue(ua.CheckToken("FDSDF"));
            Assert.IsFalse(ua.CheckToken("ODFI"));
        }
    }

    public class TestUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}