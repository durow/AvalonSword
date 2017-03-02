using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class InsertGenerator : SqlGenerator
    {
        private const string verb = "I";
        private string fields;
        private string except;
        private object param;
        private IEnumerable<object> paramList;

        internal InsertGenerator()
        { }

        public InsertGenerator(object item)
        {
            if (item == null)
                throw new NullReferenceException("item can't be null");

            param = item;
            TableName = item.GetType().Name;
        }

        protected override string GetKey()
        {
            var key = verb + TableName + fields + except;
            if (param != null)
                key += param.GetType().Name;

            return key;
        }

        public InsertGenerator SetList<T>(IEnumerable<T> list)
        {
            if (list.Count() == 0)
                throw new Exception("list count must > 0");

            param = list.FirstOrDefault();
            paramList = list as IEnumerable<object>;
            TableName = param.GetType().Name;

            return this;
        }

        public InsertGenerator Into(string tableName)
        {
            TableName = tableName;
            return this;
        }

        public InsertGenerator Fields(string fields)
        {
            this.fields = fields;
            return this;
        }

        public InsertGenerator Except(string except)
        {
            this.except = except;
            return this;
        }

        public int Go(IDbTransaction transaction = null)
        {
            return Go(Connection, transaction);
        }

        public int Go(IDbConnection connection, IDbTransaction transaction = null)
        {
            var sql = GetSQL();

            if (paramList == null)
                return SqlExecuter.ExecuteNonQuery(sql, connection, param, transaction);
            else
                return SqlExecuter.InsertList(sql, connection, paramList, transaction);
        }

        protected override string GenerateSQL()
        {
            var fieldsValues = CreateFieldsValues();
            return $"INSERT INTO {TableName}{fieldsValues}";
        }

        private string CreateFieldsValues()
        {
            if (string.IsNullOrEmpty(fields) || fields == "*")
                return CreateFieldsValuesByEntity();
            else
                return CreateFieldsValuesByFields();
        }

        private string CreateFieldsValuesByFields()
        {
            var values = $"@{fields.Replace(",", ",@")}";
            return $"({fields}) VALUES({values})";
        }

        private string CreateFieldsValuesByEntity()
        {
            if (param == null) return string.Empty;

            var exceptList = except.Trim().Split(',');
            var type = param.GetType();
            var fieldList = new List<string>();
            foreach (var property in type.GetProperties())
            {
                if (!exceptList.Contains(property.Name))
                    fieldList.Add(property.Name);
            }
            var fieldsPart = string.Join(",", fieldList);
            var valuesPart = $"@{string.Join(",@", fieldList)}";
            return $"({fieldsPart}) VALUES({valuesPart})";
        }

        private string[] GetExceptList()
        {
            if (string.IsNullOrEmpty(except))
                return new string[] { };
            else
                return except.Trim().Split(',');
        }
    }
}
