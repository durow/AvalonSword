using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class SelectGenerator:SqlGenerator
    {
        private const string verb = "S";
        private string fields;
        private string where;
        private Dictionary<string, string> join = new Dictionary<string, string>();
        private int top = 0;
        private int limit = 0;

        public SelectGenerator()
        {

        }

        public SelectGenerator(string tableName)
        {
            TableName = tableName;
        }

        protected override string GetKey()
        {
            return verb + TableName + fields + GetJoinString() + where + top + limit;
        }

        protected override string GenerateSQL()
        {
            if (string.IsNullOrEmpty(TableName))
                throw new DataException("TableName can't be empty");

            return $"SELECT{GetTopString()} {fields} FROM {TableName}{GetJoinPart()}{GetWherePart()}{GetLimitPart()}";
        }

        public SelectGenerator Fields(string fields)
        {
            this.fields = fields;
            return this;
        }

        public SelectGenerator From(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("table name can't be null");

            TableName = tableName;
            return this;
        }

        public SelectGenerator Where(string where)
        {
            this.where = where;
            return this;
        }

        public SelectGenerator JoinOn(string joinTable, string on)
        {
            join.Add(joinTable, on);
            return this;
        }

        public SelectGenerator Top(int top)
        {
            if (top <= 0)
                throw new Exception("top can't <=0");

            this.top = top;
            return this;
        }

        public SelectGenerator Limit(int limit)
        {
            if (limit <= 0)
                throw new Exception("limit can't <=0");

            this.limit = limit;
            return this;
        }

        public IEnumerable<T> Go<T>(object parameters = null, IDbTransaction transaction = null)
        {
            return Go<T>(Connection, parameters, transaction);
        }

        public IEnumerable<T> Go<T>(IDbConnection connection, object parameters = null, IDbTransaction transaction = null)
        {
            var sql = GetSQL();
            return SqlExecuter.Execute<T>(sql, connection, parameters, transaction);
        }

        public IEnumerable<dynamic> GoDynamic(object parameters = null, IDbTransaction transaction = null)
        {
            return GoDynamic(Connection, parameters, transaction);
        }

        public IEnumerable<dynamic> GoDynamic(IDbConnection connection, object parameters = null, IDbTransaction transaction = null)
        {
            var sql = GetSQL();
            return SqlExecuter.Execute(sql, connection, parameters, transaction);
        }

        private string GetWherePart()
        {
            if (string.IsNullOrEmpty(where))
                return string.Empty;

            return $" WHERE {where}";
        }

        private string GetLimitPart()
        {
            if (limit < 0)
                throw new Exception("LIMIT can't <0");

            if (limit == 0) return string.Empty;

            return $" LIMIT {limit}";
        }

        private string GetJoinPart()
        {
            if (join.Count == 0) return string.Empty;
            var result = new StringBuilder();
            foreach (var item in join)
            {
                if (string.IsNullOrEmpty(item.Value))
                    throw new Exception("ON part is empty");

                var on = item.Value.Trim().Split('=');
                if(on.Length == 1)
                    result.Append($" JOIN {item.Key} ON {TableName}.{item.Value}={item.Key}.{item.Value}");
                else
                    result.Append($" JOIN {item.Key} ON {TableName}.{on[0]}={item.Key}.{on[1]}");
            }
            return result.ToString();
        }

        private string GetTopString()
        {
            if (top < 0)
                throw new Exception("TOP can't <0");

            if (top == 0) return string.Empty;

            return $" TOP {top}";
        }

        private string GetJoinString()
        {
            var result = new StringBuilder();
            foreach (var item in join)
            {
                result.Append(item.Key).Append(item.Value);
            }
            return result.ToString();
        }
    }
}
