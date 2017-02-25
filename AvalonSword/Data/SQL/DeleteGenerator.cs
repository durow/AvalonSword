using System.Data;

namespace Ayx.AvalonSword.Data
{
    public class DeleteGenerator : SqlGenerator
    {
        private const string verb = "D";
        private string tableName;
        private string whereField;

        public DeleteGenerator(string tableName)
        {
            this.tableName = tableName;
        }

        public override string GetKey()
        {
            return verb + tableName + whereField;
        }

        public override string GetSQL()
        {
            var key = GetKey();
            if (SqlCache.ContainsKey(key))
                return SqlCache[key];
            else
            {
                var result = GenerateSQL();
                SqlCache[key] = result;
                return result;
            }
        }

        public DeleteGenerator From(string tableName)
        {
            this.tableName = tableName;
            return this;
        }

        public DeleteGenerator Where(string whereField)
        {
            this.whereField = whereField;
            return this;
        }

        public int Go(object parameters, IDbTransaction transaction)
        {
            return Go(Connection, parameters, transaction);
        }

        public int Go(IDbConnection connection, object parameters = null, IDbTransaction transaction = null)
        {
            var sql = GetSQL();
            return SqlExecuter.ExecuteNonQuery(sql, connection, parameters, transaction);
        }

        private string GenerateSQL()
        {
            return $"DELETE {tableName} {whereField}";
        }
    }
}
