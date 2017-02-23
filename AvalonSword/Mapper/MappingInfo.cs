using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ayx.AvalonSword.Mapper
{
    public class MappingInfo
    {
        public Type Src { get; private set; }
        public Type Dest { get; private set; }
        public readonly Dictionary<PropertyInfo, PropertyInfo> Properties = new Dictionary<PropertyInfo, PropertyInfo>();
        private Dictionary<string, string> nameMapping = new Dictionary<string, string>();

        public MappingInfo(Type src, Type dest, Dictionary<string,string> nameMapping)
        {
            Src = src;
            Dest = dest;
            if(nameMapping != null)
                this.nameMapping = nameMapping;
            CheckProperties();
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

                Properties.Add(srcProperty, destProperty);
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
