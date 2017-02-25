using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword.Data
{
    public class SqlBuilder<T>
    {
        public string TableName { get; private set; } = typeof(T).Name;

        SelectGenerator Select(string fields)
        {
            return new SelectGenerator();
        }

        UpdateGenerator Update(string fields)
        {
            return new UpdateGenerator();
        }

        InsertGenerator Insert(object entity)
        {
            return new InsertGenerator();
        }

        DeleteGenerator Delete()
        {
            return new DeleteGenerator(TableName);
        }
    }
}
