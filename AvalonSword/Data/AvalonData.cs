using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class AvalonData
    {
        private ISqlExecuter sqlExecuter;

        public AvalonData(ISqlExecuter sqlExecuter)
        {
            if (sqlExecuter != null)
                this.sqlExecuter = sqlExecuter;
            else
                this.sqlExecuter = new SqlExecuter();
        }

        public SelectGenerator Select<T>(string fields = "*")
        {
            return new SelectGenerator(typeof(T).Name)
            { SqlExecuter = sqlExecuter }.Fields(fields);
        }

        public SelectGenerator Select(string fields = "*")
        {
            return new SelectGenerator()
            { SqlExecuter = sqlExecuter }.Fields(fields);
        }

        public UpdateGenerator Update<T>(T item = null) where T : class
        {
            if (item is string)
                return Update(item as string, null);

            return new UpdateGenerator(typeof(T).Name, item)
            { SqlExecuter = sqlExecuter };
        }

        public UpdateGenerator Update(string tableName, object item = null)
        {
            return new UpdateGenerator(tableName, item)
            { SqlExecuter = sqlExecuter };
        }

        public InsertGenerator Insert<T>(T entity) where T:class
        {
            return new InsertGenerator(entity)
            { SqlExecuter = sqlExecuter };
        }

        public InsertGenerator InsertList<T>(IEnumerable<T> itemList)
        {
            return new InsertGenerator()
            { SqlExecuter = sqlExecuter }
            .SetList(itemList);
        }

        public DeleteGenerator Delete()
        {
            return new DeleteGenerator()
            { SqlExecuter = sqlExecuter };
        }

        public DeleteGenerator Delete<T>(T item = null) where T : class
        {
            return new DeleteGenerator(item)
            { SqlExecuter = sqlExecuter }
            .From(typeof(T).Name);
        }
    }
}
