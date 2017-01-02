/*
 * Author:durow
 * implement IConfigFile by xml file
 * Date:2017.01.02
 */

using System;
using System.IO;
using System.Xml;

namespace Ayx.AvalonSword
{
    public class XmlConfig:IConfig
    {
        /// <summary>
        /// filename of the config file
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// xml object
        /// </summary>
        public XmlDocument Doc { get; private set; }

        /// <summary>
        /// name of the config file.
        /// if the specific file dosn't exists,it can creates an empty one automatically
        /// </summary>
        /// <param name="filename"></param>
        public XmlConfig(string filename)
        {
            FileName = filename;

            if (!File.Exists(filename))
            {
                CreateEmpty();
            }

            Reload();
        }

        public XmlConfig(string filename, string xmlText)
        {
            FileName = filename;
            var doc = new XmlDocument();
            doc.LoadXml(xmlText);
            doc.Save(FileName);

            Reload();
        }

        #region Interface Methods

        /// <summary>
        /// get or set the config value by string
        /// </summary>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        public string this[string path]
        {
            get { return Get(path); }

            set { AddOrSet(path, value); }
        }

        /// <summary>
        /// add new config with value
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        /// <returns>if add is successful</returns>
        public void Add(string path, string value)
        {
            if (string.IsNullOrEmpty(path))
                throw new Exception("path can't be null!");

            var node = GetNode(path);
            if (node != null) 
                throw new Exception($"path:{path} already exits!");

            var nodeStringList = StandardPath(path).Split('/');
            var parent = Doc.SelectSingleNode("Root");
            if (parent == null)
                throw new Exception("can't find Root elment!");

            for (var i = 1; i < nodeStringList.Length; i++)
            {
                parent = AddChild(parent, nodeStringList[i]);
            }

            parent.InnerText = value;
            Doc.Save(FileName);
        }

        /// <summary>
        /// set config value,if not exists it can create a new path and set it with the value
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        public void AddOrSet(string path, string value)
        {
            var node = GetNode(path);

            if (node == null)
                Add(path, value);
            else
                Set(path, value);
        }

        /// <summary>
        /// create an empty config file
        /// </summary>
        /// <param name="filename">config filename</param>
        public void CreateEmpty()
        {
            Doc = new XmlDocument();
            var declare = Doc.CreateXmlDeclaration("1.0", "utf-8", null);
            var root = Doc.CreateElement("Root");
            Doc.AppendChild(declare);
            Doc.AppendChild(root);
            Doc.Save(FileName);
        }

        /// <summary>
        /// delete config path
        /// </summary>
        /// <param name="path">config path</param>
        public void Delete(string path)
        {
            var node = GetNode(path);
            if (node == null) return;

            node.ParentNode?.RemoveChild(node);
            Doc.Save(FileName);
        }

        /// <summary>
        /// get config value with type T
        /// </summary>
        /// <typeparam name="T">value type</typeparam>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        public T Get<T>(string path)
        {
            var value = Get(path);
            return (T) Convert.ChangeType(value, typeof(T));
        }

        /// <summary>
        /// get config value with string,you can use config["path"] instead
        /// </summary>
        /// <param name="path">config path</param>
        /// <returns>config value</returns>
        public string Get(string path)
        {
            var node = Doc.SelectSingleNode(StandardPath(path));
            if(node == null)
                throw new Exception($"path:{path} not found!");

            return node.InnerText;
        }

        /// <summary>
        /// set config value,if config path doesn't exist ,do nothing and return false
        /// you can use config["path"] = value instead.
        /// </summary>
        /// <param name="path">config path</param>
        /// <param name="value">config value</param>
        /// <returns>if set is successful</returns>
        public void Set(string path, string value)
        {
            var node = GetNode(path);
            if (node == null) 
                throw new Exception($"path:{path} not found!");

            node.InnerText = value;
            Doc.Save(FileName);
        }

        public void Reload()
        {
            Doc = new XmlDocument();
            Doc.Load(FileName);
        }

        #endregion

        #region PrivateMethods

        private XmlNode GetNode(string path)
        {
            return Doc?.SelectSingleNode(StandardPath(path)) ?? null;
        }

        private static string StandardPath(string path)
        {
            path = path.Replace(":", "/");
            path = path.Replace(".", "/");
            return "Root/" + path;
        }

        private XmlNode AddChild(XmlNode parent, string childName)
        {
            var result = parent.SelectSingleNode(childName);
            if (result != null) return result;

            result = Doc.CreateElement(childName);
            parent.AppendChild(result);
            return result;
        }

        #endregion
    }
}