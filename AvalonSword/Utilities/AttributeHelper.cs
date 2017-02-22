using System;
using System.Collections.Generic;
using System.Linq;

using System.Reflection;

namespace Ayx.AvalonSword.Utilities
{
    public class AttributeHelper
    {
        public static T GetAttribute<T>(PropertyInfo property, bool inherit=false) where T :Attribute
        {
            return property.GetCustomAttributes(typeof(T), inherit).FirstOrDefault() as T;
        }
    }
}
