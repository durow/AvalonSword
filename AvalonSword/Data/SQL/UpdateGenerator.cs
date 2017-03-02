using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Ayx.AvalonSword.Data
{
    public class UpdateGenerator : SqlGenerator
    {
        private const string verb = "U";
        private string fields;
        private string where;
        private object entity;
        private string except;
        private string key;

        public UpdateGenerator(string tableName, object entity = null)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("table name can't be null");

            this.entity = entity;
            TableName = tableName;
        }

        protected override string GenerateSQL()
        {
            var set = CreateSet();
            return $"UPDATE {TableName} SET {set}{GetWherePart()}";
        }

        protected override string GetKey()
        {
            return verb + TableName + fields + where + except;
        }

        public UpdateGenerator Set(string fields)
        {
            if (fields == null)
                throw new Exception("fields can't be null");

            this.fields = fields;
            return this;
        }

        public UpdateGenerator Key(string keyField)
        {
            this.key = keyField;
            return this;
        }

        public UpdateGenerator Where(string where)
        {
            this.where = where;
            return this;
        }

        public UpdateGenerator Except(string except)
        {
            this.except = except;
            return this;
        }

        public int Go(object parameters = null, IDbTransaction transaction = null)
        {
            if (parameters == null)
                parameters = entity;

            return Go(Connection, parameters, transaction);
        }

        public int Go(IDbConnection connection, object parameters, IDbTransaction transaction = null)
        {
            var sql = GetSQL();
            return SqlExecuter.ExecuteNonQuery(sql, connection, parameters, transaction);
        }

        private string CreateSet()
        {
            if (string.IsNullOrEmpty(fields))
                return CreateSetFromEntity();
            else
                return CreateSetFromFields();
        }

        private string UpdateFieldFormat(string field)
        {
            if (field.Contains("=@"))
                return field;
            else
                return $"{field}=@{field}";
        }

        private string CreateSetFromFields()
        {
            var fieldList = new List<string>();

            if (!string.IsNullOrEmpty(fields))
                fieldList.AddRange(fields.Trim().Split(','));

            for (int i = 0; i < fieldList.Count; i++)
            {
                fieldList[i] = UpdateFieldFormat(fieldList[i]);
            }

            return string.Join(",", fieldList);
        }

        private string CreateSetFromEntity()
        {
            var result = new List<string>();

            List<string> exceptList = new List<string>();

            if (!string.IsNullOrEmpty(except))
                exceptList.AddRange(except.Trim().Split(','));

            if (!string.IsNullOrEmpty(key))
                exceptList.Add(key);

            var type = entity.GetType();
            foreach (var property in type.GetProperties())
            {
                if (exceptList.Contains(property.Name))
                    continue;
                result.Add(UpdateFieldFormat(property.Name));
            }
            return string.Join(",", result);
        }

        private string GetWherePart()
        {
            if (string.IsNullOrEmpty(where) && string.IsNullOrEmpty(key))
                return string.Empty;
            else if (string.IsNullOrEmpty(key))
                return $" WHERE {where}";
            else if (string.IsNullOrEmpty(where))
                return $" WHERE {key}=@{key}";
            else
                return $" WHERE {key}=@{key} AND {where}";
        }
    }
}
