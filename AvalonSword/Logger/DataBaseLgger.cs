using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Logger
{
    public class DataBaseLgger:LogWriterBase
    {
        private IDbConnection dbConn;
        private IDbCommand cmd;
        private IDbTransaction transaction;

        public DataBaseLgger(IDbConnection connection)
        {
            dbConn = connection;
            cmd = CreateCommand();
        }

        public override void WriteLog(LogInfo logInfo)
        {
            if(transaction == null)
            {
                if (dbConn.State != ConnectionState.Open)
                    dbConn.Open();
                transaction = dbConn.BeginTransaction();
                cmd.Transaction = transaction;
            }

            cmd.Parameters["@LogDateTime"] = logInfo.LogDateTime.ToString(DateTimeFormat);
            cmd.Parameters["@LogLevel"] = logInfo.LogLevel.ToString();
            cmd.Parameters["@Module"] = logInfo.Module;
            cmd.Parameters["@Method"] = logInfo.Methoid;
            cmd.Parameters["@LogContent"] = logInfo.LogContent;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
                dbConn.Close();
                throw;
            }
        }

        private void Commit()
        {
            if (transaction == null)
                return;

            transaction.Commit();
            dbConn.Close();
            transaction = null;
        }

        private void CheckLogTable()
        {
            const string sql = @"CREATE TABLE Log IF NOT EXISTS";
            var cmd = dbConn.CreateCommand();
            try
            {
                dbConn.Open();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }
        }

        private IDbCommand CreateCommand()
        {
            const string sql = @"INSERT INTO Log(LogDateTime,LogLevel,Module,Method,LogContent) 
                                            VALUES(@LogDateTime,@LogLevel,@Module,@Method,@LogContent)";
            var cmd = dbConn.CreateCommand();
            cmd.CommandText = sql;
            AddParameter(cmd, "@LogDateTime", null);
            AddParameter(cmd, "@LogLevel", null);
            AddParameter(cmd, "@Module", null);
            AddParameter(cmd, "@Method", null);
            AddParameter(cmd, "@LogContent", null);

            return cmd;
        }

        private void AddParameter(IDbCommand cmd, string key, object value)
        {
            var p = cmd.CreateParameter();
            p.ParameterName = key;
            p.Value = value;
            cmd.Parameters.Add(p);
        }
    }
}
