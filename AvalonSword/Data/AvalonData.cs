﻿using System;
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
            return new UpdateGenerator(typeof(T).Name, item)
            { SqlExecuter = sqlExecuter };
        }

        public UpdateGenerator Update(string tableName, object item = null)
        {
            return new UpdateGenerator(tableName, item)
            { SqlExecuter = sqlExecuter };
        }

        public InsertGenerator Insert<T>(T entity)
        {
            return new InsertGenerator(entity)
            { SqlExecuter = sqlExecuter };
        }

        public InsertGenerator Insert<T>(IEnumerable<T> itemList)
        {
            return new InsertGenerator(itemList)
            { SqlExecuter = sqlExecuter };
        }

        public DeleteGenerator DeleteFrom(string tableName)
        {
            return new DeleteGenerator(tableName)
            { SqlExecuter = sqlExecuter };
        }

        public DeleteGenerator DeleteFrom<T>()
        {
            return new DeleteGenerator(typeof(T).Name)
            { SqlExecuter = sqlExecuter };
        }
    }
}