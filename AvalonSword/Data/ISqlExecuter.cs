using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Ayx.AvalonSword.Data
{
    public interface ISqlExecuter
    {
        int ExecuteNonQuery(string sql, IDbConnection connection, object parameters, IDbTransaction transaction);
        object ExecuteScalar(string sql, IDbConnection connection, object parameters, IDbTransaction transaction);
        IEnumerable<T> Execute<T>(string sql, IDbConnection connection, object parameters, IDbTransaction transaction);
        IEnumerable<dynamic> Execute(string sql, IDbConnection connection, object parameters, IDbTransaction transaction);
        int InsertList(string sql, IDbConnection connection, IEnumerable<object> parameters, IDbTransaction transaction);
    }
}
