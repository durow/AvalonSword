/*
 * Author:durow
 * user and authentication
 * Date:2017.02.12
 */

using System.Collections.Generic;

namespace Ayx.AvalonSword
{
    public class UserAuthentication<TUser> : IUserAuthentication<TUser>
        where TUser : class
    {
        public string Token { get; set; }
        public int Ring { get; set; }
        public List<string> Roles { get; set; }
        public List<string> Groups { get; set; }
        public TUser User { get; set; }

        public UserAuthentication()
        {
            Roles = new List<string>();
            Groups = new List<string>();
        }

        public bool CheckGroup(string group)
        {
            return Groups.Contains(group);
        }

        public IEnumerable<string> CheckInGroups(IEnumerable<string> groups)
        {
            foreach (var group in groups)
            {
                if (Groups.Contains(group))
                    yield return group;
            }
        }

        public IEnumerable<string> CheckInRoles(IEnumerable<string> roles)
        {
            foreach (var role in roles)
            {
                if (Roles.Contains(role))
                    yield return role;
            }
        }

        public bool CheckRole(string role)
        {
            return Roles.Contains(role);
        }

        public bool CheckRing(int ring)
        {
            return Ring <= ring;
        }

        public bool CheckToken(string token)
        {
            return Token == token;
        }
    }
}
