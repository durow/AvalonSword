using System;
using System.Data;

namespace Ayx.AvalonSword.Data
{
    public class UpdateGenerator : SqlGenerator
    {
        private const string verb = "U";
        private string fields;
        private string where;

        public UpdateGenerator(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("table name can't be null");

            TableName = tableName;
        }

        protected override string GenerateSQL()
        {
            var set = CreateSet();
            return $"UPDATE {TableName} SET {set} {where}";
        }

        protected override string GetKey()
        {
            return verb + TableName + fields + where;
        }

        public UpdateGenerator Set(string fields)
        {
            if (fields == null)
                throw new Exception("fields can't be null");

            this.fields = fields;
            return this;
        }

        public UpdateGenerator Where(string where)
        {
            this.where = where;
            return this;
        }

        public int Go(object parameters, IDbTransaction transaction)
        {
            return Go(Connection, parameters, transaction);
        }

        public int Go(IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var sql = GetSQL();
            return SqlExecuter.ExecuteNonQuery(sql, connection, parameters, transaction);
        }

        private string CreateSet()
        {
            var fieldList = fields.Trim().Split(',');
            for (int i = 0; i < fieldList.Length; i++)
            {
                fieldList[i] = UpdateFieldFormat(fieldList[i]);
            }

            return string.Join(",", fieldList);
        }

        private string UpdateFieldFormat(string field)
        {
            if (field.Contains("=@"))
                return field;
            else
                return $"{field}=@{field}";
        }
    }
}
