using Ayx.AvalonSword.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Ayx.AvalonSword.WebService
{
    public class WebAPI : IWebAPI
    {
        private IJson json;

        public WebClient WebClient { get; private set; } = new WebClient();

        public WebAPI(IJson json)
        {
            this.json = json;
        }

        #region GET

        public T GetObject<T>(string url) where T : class
        {
            var text = GetString(url);
            return json.Parse<T>(text);
        }

        public string GetString(string url)
        {
            return WebClient.DownloadString(url);
        }

        public dynamic GetDynamic(string url)
        {
            var text = GetString(url);
            return json.ParseDynamic(text);
        }

        public byte[] GetData(string url)
        {
            return WebClient.DownloadData(url);
        }

        public MatchCollection GetRegex(string url, string regex)
        {
            var text = GetString(url);
            var reg = new Regex(regex);
            return reg.Matches(text);
        }

        #endregion

        #region POST
        public T PostObject<T>(string url, IDictionary<string,object> data) where T:class
        {
            return PostObject<T>(url, GetData(data));
        }

        public T PostObject<T>(string url, object data) where T:class
        {
            return PostObject<T>(url, GetData(data));
        }

        public T PostObject<T>(string url, string data) where T : class
        {
            return SendObject<T>(url, data, "POST");
        }

        public string PostString(string url, IDictionary<string,object> data)
        {
            return PostString(url, GetData(data));
        }

        public string PostString(string url, object data)
        {
            return PostString(url, GetData(data));
        }

        public string PostString(string url, string data)
        {
            return SendString(url, data, "POST");
        }

        public dynamic PostDynamic(string url, object data)
        {
            return PostDynamic(url, GetData(data));
        }

        public dynamic PostDynamic(string url, string data)
        {
            return SendDynamic(url, data, "POST");
        }

        public byte[] PostData(string url, byte[] data)
        {
            return SendData(url, data, "POST");
        }

        public MatchCollection PostRegex(string url, string data, string regex)
        {
            return SendRegex(url, data, "POST", regex);
        }

        #endregion

        #region DELETE

        public T DeleteObject<T>(string url, string data) where T : class
        {
            throw new NotImplementedException();
        }

        public string DeleteString(string url, string data)
        {
            return SendString(url, data, "DELETE");
        }

        public dynamic DeleteDynamic(string url, string data)
        {
            return SendDynamic(url, data, "DELETE");
        }

        public MatchCollection DeleteRegex(string url, string data, string regex)
        {
            return SendDynamic(url, data, "DELETE");
        }

        #endregion

        #region PUT

        public T PutObject<T>(string url, string data) where T : class
        {
            return SendObject<T>(url, data, "PUT");
        }

        public string PutString(string url, string data)
        {
            return SendString(url, data, "PUT");
        }

        public dynamic PutDynamic(string url, string data)
        {
            return SendDynamic(url, data, "PUT");
        }

        public MatchCollection PutRegex(string url, string data, string regex)
        {
            return SendRegex(url, data, "PUT", regex);
        }

        #endregion

        #region Send

        public T SendObject<T>(string url, object data, string method = "POST") where T:class
        {
            return SendObject<T>(url, GetData(data), method);
        }

        public T SendObject<T>(string url, IDictionary<string,object> data, string method) where T : class
        {
            return SendObject<T>(url, GetData(data), method = "POST");
        }

        public T SendObject<T>(string url, string data, string method = "POST") where T : class
        {
            var text = WebClient.UploadString(url, method, data);
            return json.Parse<T>(text);
        }

        public string SendString(string url, IDictionary<string,object> data, string method = "POST")
        {
            return SendString(url, GetData(data), method);
        }

        public string SendString(string url, object data, string method = "POST")
        {
            return SendString(url, GetData(data), method);
        }

        public string SendString(string url, string data, string method = "POST")
        {
            return WebClient.UploadString(url, method, data);
        }

        public dynamic SendDynamic(string url, IDictionary<string,object> data, string method = "POST")
        {
            return SendDynamic(url, GetData(data), method);
        }

        public dynamic SendDynamic(string url, object data, string method = "POST")
        {
            return SendDynamic(url, GetData(data), method);
        }

        public dynamic SendDynamic(string url, string data, string method = "POST")
        {
            var text = SendString(url, data, method);
            return json.ParseDynamic(text);
        }

        public byte[] SendData(string url, byte[] data, string method = "POST")
        {
            return WebClient.UploadData(url, method, data);
        }

        public MatchCollection SendRegex(string url, IDictionary<string,object> data, string regex, string method = "POST")
        {
            return SendRegex(url, GetData(data), method, regex);
        }

        public MatchCollection SendRegex(string url, object data, string regex, string method = "POST")
        {
            return SendRegex(url, GetData(data), method, regex);
        }

        public MatchCollection SendRegex(string url, string data, string regex, string method = "POST")
        {
            var text = SendString(url, data, method);
            var reg = new Regex(regex);
            return reg.Matches(text);
        }

        #endregion

        #region File

        public void DownloadFile(string url, string local)
        {
            WebClient.DownloadFile(url, local);
        }

        public void DownloadFileAsync(string url, string local, object userToken = null)
        {
            WebClient.DownloadFileAsync(new Uri(url), local, userToken);
        }

        public byte[] UploadFile(string url, string local, string method = "POST")
        {
            return WebClient.UploadFile(url, method, local);
        }

        public void UploadFileAsync(string url, string local, string method = "POST", object userToken = null)
        {
            WebClient.UploadFileAsync(new Uri(url), method, local, userToken);
        }

        #endregion

        private string GetData(object o)
        {
            var list = new List<string>();
            foreach (var property in o.GetType().GetProperties())
            {
                list.Add($"{property.Name}={property.GetValue(o, null)}");
            }
            return string.Join("&", list);
        }

        private string GetData(IDictionary<string,object> list)
        {
            var result = new List<string>();
            foreach (var item in list)
            {
                result.Add($"{item.Key}={item.Value}");
            }
            return string.Join("&", result);
        }
    }
}
