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

        SelectGenerator Select(string fields)
        {
            return new SelectGenerator();
        }

        UpdateGenerator Update(string fields)
        {
            return new UpdateGenerator();
        }

        InsertGenerator Insert<T>(T entity)
        {
            return new InsertGenerator(entity);
        }

        DeleteGenerator Delete()
        {
            return new DeleteGenerator();
        }

        DeleteGenerator Delete<T>()
        {
            return new DeleteGenerator(typeof(T).Name);
        }
    }
}
