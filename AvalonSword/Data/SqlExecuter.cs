using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Threading.Tasks;

namespace Ayx.AvalonSword.Data
{
    public class SqlExecuter : ISqlExecuter
    {
        public IEnumerable<dynamic> Execute(string sql, IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var result = new List<dynamic>();
            var cmd = GetCommand(sql, connection, parameters, transaction);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(DataReaderToDynamic(reader));
            }

            reader.Close();
            connection.Close();
            return result;
        }

        public IEnumerable<T> Execute<T>(string sql, IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var propList = typeof(T).GetProperties();
            var result = new List<T>();
            var cmd = GetCommand(sql, connection, parameters, transaction);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result.Add(DataReaderToObjectr<T>(reader, propList));
            }

            reader.Close();
            connection.Close();
            return result;
        }

        public int ExecuteNonQuery(string sql, IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var cmd = GetCommand(sql, connection, parameters, transaction);
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            var result = cmd.ExecuteNonQuery();
            connection.Close();
            return result;
        }

        public object ExecuteScalar(string sql, IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var cmd = GetCommand(sql, connection, parameters, transaction);
            if (connection.State == ConnectionState.Closed)
                connection.Open();
            var result = cmd.ExecuteScalar();
            connection.Close();
            return result;
        }

        public IDbCommand GetCommand(string sql, IDbConnection connection, object parameters, IDbTransaction transaction)
        {
            var cmd = connection.CreateCommand();
            AddCommandParameters(cmd, parameters);
            cmd.CommandText = sql;
            cmd.Transaction = transaction;
            return cmd;
        }

        public int InsertList(string sql, IDbConnection connection, IEnumerable<object> parameters, IDbTransaction transaction)
        {
            var result = 0;
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            if (transaction == null)
                transaction = connection.BeginTransaction();

            try
            {
                foreach (var item in parameters)
                {
                    var cmd = GetCommand(sql, connection, item, transaction);
                    result += cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        private void AddCommandParameters(IDbCommand cmd, object parameters)
        {
            if (parameters == null) return;
            var type = parameters.GetType();
            foreach (var property in type.GetProperties())
            {
                var param = cmd.CreateParameter();
                param.ParameterName = "@" + property.Name;
                param.Value = property.GetValue(parameters, null);
                cmd.Parameters.Add(param);
            }
        }

        private dynamic DataReaderToDynamic(IDataReader reader)
        {
            dynamic result = new ExpandoObject();
            var dict = result as IDictionary<string, object>;
            if (dict == null) return null;

            for (int i = 0; i < reader.FieldCount; i++)
            {
                dict[reader.GetName(i)] = reader[i];
            }

            return result;
        }

        private T DataReaderToObjectr<T>(IDataReader reader, PropertyInfo[] propertyList)
        {
            var result = Activator.CreateInstance<T>();
            foreach (var property in propertyList)
            {
                var index = reader.GetOrdinal(property.Name);
                if (index == -1) continue;

                property.SetValue(result, reader.GetValue(index), null);
            }
            return result;
        }
    }
}
