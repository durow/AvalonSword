using System.Data;

namespace Ayx.AvalonSword.Data
{
    public class DeleteGenerator : SqlGenerator
    {
        private const string verb = "D";
        private string where;
        private object item;
        private string key;

        public DeleteGenerator() { }

        public DeleteGenerator(object item)
        {
            this.item = item;
        }

        protected override string GetKey()
        {
            return verb + TableName + key + where;
        }

        public DeleteGenerator From(string tableName)
        {
            TableName = tableName;
            return this;
        }

        public DeleteGenerator Where(string whereField)
        {
            this.where = whereField;
            return this;
        }

        public DeleteGenerator Key(string keyField)
        {
            key = keyField;
            return this;
        }

        public int Go(object parameters = null, IDbTransaction transaction = null)
        {
            return Go(Connection, parameters, transaction);
        }

        public int Go(IDbConnection connection, object parameters = null, IDbTransaction transaction = null)
        {
            if (parameters == null)
                parameters = item;

            var sql = GetSQL();
            return SqlExecuter.ExecuteNonQuery(sql, connection, parameters, transaction);
        }

        protected override string GenerateSQL()
        {
            return $"DELETE FROM {TableName}{GetWherePart()}";
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
