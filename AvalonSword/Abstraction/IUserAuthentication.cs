/*
 * Author:durow
 * interface of UserAuthentication
 * Date:2017.02.12
 */

using System.Collections.Generic;

namespace Ayx.AvalonSword
{
    public interface IUserAuthentication<TUser> where TUser : class
    {
        string Token { get; set; }
        int Ring { get; set; }
        TUser User { get; set; }
        List<string> Roles { get; set; }
        List<string> Groups { get; set; }
        bool CheckRole(string role);
        IEnumerable<string> CheckInRoles(IEnumerable<string> roles);
        bool CheckGroup(string group);
        IEnumerable<string> CheckInGroups(IEnumerable<string> groups);
        bool CheckRing(int ring);
        bool CheckToken(string token);
    }
}
