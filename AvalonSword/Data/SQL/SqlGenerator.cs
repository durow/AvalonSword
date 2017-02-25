using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public abstract class SqlGenerator
    {
        public string TableName { get; set; }
        protected static Dictionary<string, string> SqlCache = new Dictionary<string, string>();
        protected static Dictionary<string, ObjectMapping> OM = new Dictionary<string, ObjectMapping>();
        public static ISqlExecuter SqlExecuter { get; set; } = new SqlExecuter();
        public IDbConnection Connection { get; set; }

        public string GetSQL()
        {
            var key = GetKey();
            if (!SqlCache.ContainsKey(key))
                SqlCache[key] = GenerateSQL();

            return SqlCache[key];
        }
        protected abstract string GetKey();

        protected abstract string GenerateSQL();

        public static ObjectMapping MapTable(string tableName)
        {
            var mapping = new ObjectMapping(tableName);
            OM.Add(tableName, mapping);
            return mapping;
        }
    }
}
