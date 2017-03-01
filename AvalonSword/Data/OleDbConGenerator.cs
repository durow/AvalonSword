using System.Data;
using System.Data.OleDb;

namespace Ayx.AvalonSword.Data
{
    public class OleDbConGenerator : IDbConGenerator
    {
        private const string provider4 = "Provider=Microsoft.Jet.OLEDB.4.0;";
        private const string provider12 = "Provider=Microsoft.ACE.OLEDB.12.0;";
        public string ConnectionString { get; private set; }
        public IDbConnection DbConnection
        {
            get
            {
                return new OleDbConnection(ConnectionString);
            }
        }

        public OleDbConGenerator(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static OleDbConGenerator Access(string filename)
        {
            var conStr = $"{GetAccessProvider(filename)}Data Source={filename}; User Id=admin; Password=";
            return new OleDbConGenerator(conStr);
        }

        public static OleDbConGenerator Access(string filename, string password)
        {
            var conStr = $"{GetAccessProvider(filename)}Data Source={filename};Jet OLEDB:Database Password={password};";
            return new OleDbConGenerator(conStr);
        }

        public static OleDbConGenerator Excel(string filename, string version = "8.0", bool HDR = true, int IMEX = 1)
        {
            var hdr = HDR ? "Yes" : "No";

            string provider;
            if (filename.EndsWith("xls"))
                provider = provider4;
            else
                provider = provider12;

            var con = $"{provider}Data Source={filename};Extended Properties=\"Excel {version};HDR={hdr};IMEX={IMEX};\"";
            return new OleDbConGenerator(con);
        }

        private static string GetAccessProvider(string filename)
        {
            if (filename.EndsWith("mdb"))
                return provider4;
            else
                return provider12;
        }
    }
}
