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

        public bool CheckGroup(string group)
        {
            return Groups.Contains(group);
        }

        public int CheckInGroups(IList<string> groups)
        {
            var result = 0;
            foreach (var group in groups)
            {
                if (Groups.Contains(group))
                    result++;
            }
            return result;
        }

        public int CheckInRoles(IList<string> roles)
        {
            var result = 0;
            foreach (var role in roles)
            {
                if (roles.Contains(role))
                    result++;
            }
            return result;
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
