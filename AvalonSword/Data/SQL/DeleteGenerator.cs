using System.Data;

namespace Ayx.AvalonSword.Data
{
    public class DeleteGenerator : SqlGenerator
    {
        private const string verb = "D";
        private string whereField;

        public DeleteGenerator()
        { }

        public DeleteGenerator(string tableName)
        {
            TableName = tableName;
        }

        protected override string GetKey()
        {
            return verb + TableName + whereField;
        }

        public DeleteGenerator From(string tableName)
        {
            TableName = tableName;
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

        protected override string GenerateSQL()
        {
            return $"DELETE FROM {TableName} {whereField}";
        }
    }
}
