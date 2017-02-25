using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public abstract class SqlGenerator
    {
        protected static Dictionary<string, string> SqlCache = new Dictionary<string, string>();
        protected static Dictionary<string, ObjectMapping> OM = new Dictionary<string, ObjectMapping>();
        public IDbConnection Connection { get; set; }
        protected ISqlExecuter SqlExecuter;
        public abstract string GetSQL();
        public abstract string GetKey();

        public static ObjectMapping MapTable(string tableName)
        {
            var mapping = new ObjectMapping(tableName);
            OM.Add(tableName, mapping);
            return mapping;
        }
    }
}
