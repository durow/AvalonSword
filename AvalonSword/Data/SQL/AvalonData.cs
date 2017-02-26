using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class AvalonData
    {
        public AvalonData(ISqlExecuter sqlExecuter = null)
        {
            if (sqlExecuter != null)
                SqlGenerator.SqlExecuter = sqlExecuter;
        }

        public SelectGenerator Select<T>(string fields)
        {
            return new SelectGenerator(typeof(T).Name).Fields(fields);
        }

        public SelectGenerator Select(string fields)
        {
            return new SelectGenerator().Fields(fields);
        }

        public UpdateGenerator Update<T>()
        {
            return new UpdateGenerator(typeof(T).Name);
        }

        public UpdateGenerator Update(string tableName)
        {
            return new UpdateGenerator(tableName);
        }

        public InsertGenerator Insert<T>(T entity)
        {
            return new InsertGenerator(entity);
        }

        public DeleteGenerator DeleteFrom(string tableName)
        {
            return new DeleteGenerator(tableName);
        }

        public DeleteGenerator DeleteFrom<T>()
        {
            return new DeleteGenerator(typeof(T).Name);
        }
    }
}
