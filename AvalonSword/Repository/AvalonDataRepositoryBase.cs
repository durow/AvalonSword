using Ayx.AvalonSword.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Data;

namespace Ayx.AvalonSword.Repository
{
    public abstract class AvalonDataRepositoryBase<T> : IRepository<T> where T : class
    {
        public abstract AvalonData Data { get; protected set; }

        public virtual int DeleteOne(string key, object value)
        {
            var p = CreateParameter(key, value);
            return Data
                .Delete<T>()
                .Key(key)
                .Go(p);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Data.Select<T>().Go<T>();
        }

        public virtual T GetOne(string keyField, object value)
        {
            var p = CreateParameter(keyField, value);
            return Data
                .Select<T>()
                .Where($"{keyField}=@{keyField}")
                .Go<T>(p)
                .FirstOrDefault();
        }

        public virtual int Insert(T item)
        {
            return Data.Insert(item).Go();
        }

        public virtual int InsertList(IEnumerable<T> entityList)
        {
            return Data.InsertList<T>(entityList).Go();
        }

        public virtual int Update(T entity, string keyField) 
        {
            return Data.Update(entity).Except(keyField).Go();
        }

        public virtual int UpdateField(string keyField, object keyValue, string updateField, object updateValue)
        {
            return Data
                .Update<T>()
                .Set($"{updateField}=@{updateField}")
                .Where($"{keyField}=@{keyField}")
                .Go(CreateParameter(new Dictionary<string, object> {
                    {updateField,updateValue },
                    {keyField,keyValue },
                }));
        }

        public int Clear()
        {
            return Data.Delete<T>(null).Go();
        }

        protected dynamic CreateParameter(string key, object value)
        {
            dynamic result = new ExpandoObject();
            var dict = result as IDictionary<string, object>;
            dict.Add("@" + key, value);
            return result;
        }

        protected dynamic CreateParameter(IDictionary<string, object> parameters)
        {
            dynamic result = new ExpandoObject();
            var dict = result as IDictionary<string, object>;
            foreach (var p in parameters)
            {
                dict.Add("@" + p.Key, p.Value);
            }
            return result;
        }

        public abstract int CreateTable();
    }
}
