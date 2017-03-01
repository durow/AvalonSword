using System.Data;
using System.Data.SqlClient;

namespace Ayx.AvalonSword.Data
{
    public class SqlDbConGenerator : IDbConGenerator
    {
        public string ConnectionString { get; private set; }
        public IDbConnection DbConnection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }

        public SqlDbConGenerator(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public SqlDbConGenerator(
            string host,
            string dataBaseName,
            string user,
            string password)
        {
            ConnectionString = new SqlConnectionStringBuilder()
            {
                DataSource = host,
                InitialCatalog = dataBaseName,
                UserID = user,
                Password = password,
            }.ToString();
        }
    }
}
