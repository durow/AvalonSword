using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ayx.AvalonSword.Mapper
{
    public class MappingInfo
    {
        public Type Src { get; private set; }
        public Type Dest { get; private set; }
        private Dictionary<PropertyInfo, PropertyInfo> properties = new Dictionary<PropertyInfo, PropertyInfo>();
        private Dictionary<string, string> nameMapping = new Dictionary<string, string>();
        private Dictionary<string, Func<object, bool>> filters = new Dictionary<string, Func<object, bool>>();

        public MappingInfo(Type src, Type dest, Dictionary<string, string> nameMapping)
        {
            Src = src;
            Dest = dest;
            if (nameMapping != null)
                this.nameMapping = nameMapping;
            CheckProperties();
        }

        public MappingInfo AddFilter<TSrc>(string srcName, Func<TSrc, bool> func) where TSrc : class
        {
            if (string.IsNullOrEmpty(srcName))
                throw new Exception("source property name can't be null");

            filters[srcName] = (Func<object, bool>)func;
            return this;
        }

        public Dictionary<PropertyInfo, PropertyInfo> GetPropertiesMapping(object src)
        {
            if (filters.Count == 0) return properties;

            var result = new Dictionary<PropertyInfo, PropertyInfo>();
            foreach (var info in properties)
            {
                if (!filters.ContainsKey(info.Key.Name))
                    result.Add(info.Key, info.Value);
                else if (filters[info.Key.Name].Invoke(src))
                    result.Add(info.Key, info.Value);
            }
            return result;
        }

        private void CheckProperties()
        {
            var srcList = Src.GetProperties();

            foreach (var srcProperty in srcList)
            {
                var destName = GetMappingName(srcProperty.Name);
                var destProperty = Dest.GetProperty(destName);
                if (destProperty == null) continue;
                if (srcProperty.PropertyType != destProperty.PropertyType) continue;

                properties.Add(srcProperty, destProperty);
            }
        }

        private string GetMappingName(string srcName)
        {
            if (nameMapping.ContainsKey(srcName))
                return nameMapping[srcName];
            else
                return srcName;
        }
    }
}
