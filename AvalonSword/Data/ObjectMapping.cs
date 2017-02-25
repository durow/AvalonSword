using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class ObjectMapping
    {
        public string TableName { get; private set; }

        private Dictionary<string, string> PropertyToField = new Dictionary<string, string>();
        private Dictionary<string, string> FieldToProperty = new Dictionary<string, string>();

        public ObjectMapping(string tableName)
        {
            TableName = tableName;
        }

        public ObjectMapping Map(string property, string field)
        {
            CheckPropertyExists(property);
            CheckFieldExists(field);

            PropertyToField.Add(property, field);
            FieldToProperty.Add(field, property);

            return this;
        }
   
        public string GetFieldFromProperty(string property)
        {
            if (PropertyToField.ContainsKey(property))
                return PropertyToField[property];
            else
                return string.Empty;
        }

        public string GetPropertyFromField(string field)
        {
            if (FieldToProperty.ContainsKey(field))
                return FieldToProperty[field];
            else
                return string.Empty;
        }

        private void CheckPropertyExists(string property)
        {
            if (PropertyToField.ContainsKey(property))
                throw new Exception("property " + property + " has been mapped!");
        }

        private void CheckFieldExists(string field)
        {
            if (FieldToProperty.ContainsKey(field))
                throw new Exception("field " + field + " has been mapped!");
        }
    }
}
