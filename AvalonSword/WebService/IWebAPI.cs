using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Ayx.AvalonSword.WebService
{
    public interface IWebAPI
    {
        T GetObject<T>(string url) where T : class;
        string GetString(string url);
        dynamic GetDynamic(string url);
        byte[] GetData(string url);
        MatchCollection GetRegex(string url, string regex);

        T PostObject<T>(string url, string data) where T : class;
        string PostString(string url, string data);
        dynamic PostDynamic(string url, string data);
        byte[] PostData(string url, byte[] data);
        MatchCollection PostRegex(string url, string data, string regex);

        T DeleteObject<T>(string url, string data) where T : class;
        string DeleteString(string url, string data);
        dynamic DeleteDynamic(string url, string data);
        MatchCollection DeleteRegex(string url, string data, string regex);

        T PutObject<T>(string url, string data) where T : class;
        string PutString(string url, string data);
        dynamic PutDynamic(string url, string data);
        MatchCollection PutRegex(string url, string data, string regex);

        T SendObject<T>(string url, string data, string method = "POST") where T : class;
        string SendString(string url, string data, string method = "POST");
        dynamic SendDynamic(string url, string data, string method = "POST");
        byte[] SendData(string url, byte[] data, string method = "POST");
        MatchCollection SendRegex(string url, string data, string regex, string method = "POST");
    }
}
