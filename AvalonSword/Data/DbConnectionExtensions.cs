using System.Collections.Generic;
using System.Data;

namespace Ayx.AvalonSword.Data
{
    public static class DbConnectionExtensions
    {
        public static ISqlExecuter sqlExecuter = new SqlExecuter();

        public static SelectGenerator Select<T>(this IDbConnection connection, string fields = "*")
        {
            return new SelectGenerator(typeof(T).Name)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            }.Fields(fields);
        }

        public static SelectGenerator Select(this IDbConnection connection, string fields = "*")
        {
            return new SelectGenerator()
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            }.Fields(fields);
        }

        public static UpdateGenerator Update<T>(this IDbConnection connection)
        {
            return new UpdateGenerator(typeof(T).Name)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            };
        }

        public static UpdateGenerator Update(this IDbConnection connection, string tableName)
        {
            return new UpdateGenerator(tableName)
            { SqlExecuter = sqlExecuter };
        }

        public static InsertGenerator Insert<T>(this IDbConnection connection, T entity)
        {
            return new InsertGenerator(entity)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            };
        }

        public static InsertGenerator Insert<T>(this IDbConnection connection, IEnumerable<T> itemList)
        {
            return new InsertGenerator(itemList)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            };
        }

        public static DeleteGenerator DeleteFrom(this IDbConnection connection, string tableName)
        {
            return new DeleteGenerator(tableName)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            };
        }

        public static DeleteGenerator DeleteFrom<T>(this IDbConnection connection)
        {
            return new DeleteGenerator(typeof(T).Name)
            {
                Connection = connection,
                SqlExecuter = sqlExecuter
            };
        }
    }
}
