using System;
using System.Collections.Generic;
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
            if (string.IsNullOrEmpty(tableName))
                throw new Exception("table name can't be null");

            TableName = tableName;
        }

        protected override string GetKey()
        {
            return verb + TableName + fields + GetJoinString() + top + limit;
        }

        protected override string GenerateSQL()
        {
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

        private string GetWherePart()
        {
            if (string.IsNullOrEmpty(where))
                return string.Empty;

            return $" {where}";
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
                result.Append($" JOIN {item.Key} ON {item.Value}");
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
