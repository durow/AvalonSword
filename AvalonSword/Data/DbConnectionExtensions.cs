using System.Data;

namespace Ayx.AvalonSword.Data
{
    public static class DbConnectionExtensions
    {
        public static SelectGenerator Select<T>(this IDbConnection connection, string fields)
        {
            return new SelectGenerator(typeof(T).Name)
            {
                Connection = connection
            }.Fields(fields);
        }

        public static SelectGenerator Select(this IDbConnection connection, string fields)
        {
            return new SelectGenerator()
            {
                Connection = connection
            }.Fields(fields);
        }

        public static UpdateGenerator Update<T>(this IDbConnection connection)
        {
            return new UpdateGenerator(typeof(T).Name)
            {
                Connection = connection
            };
        }

        public static UpdateGenerator Update(this IDbConnection connection, string tableName)
        {
            return new UpdateGenerator(tableName);
        }

        public static InsertGenerator Insert<T>(this IDbConnection connection, T entity)
        {
            return new InsertGenerator(entity) { Connection = connection};
        }

        public static DeleteGenerator DeleteFrom(this IDbConnection connection, string tableName)
        {
            return new DeleteGenerator(tableName)
            {
                Connection = connection
            };
        }

        public static DeleteGenerator DeleteFrom<T>(this IDbConnection connection)
        {
            return new DeleteGenerator(typeof(T).Name)
            {
                Connection = connection
            };
        }
    }
}
