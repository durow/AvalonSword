using System.Collections.Generic;

namespace Ayx.AvalonSword
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetOne(string keyField, object value);
        int Insert(T item);
        int InsertList(IEnumerable<T> entityList);
        int Update(T entity, string keyField);
        int UpdateField(string keyField, object keyValue, string updateField, object updateValue);
        int DeleteOne(string key, object value);
        int Clear();
        int CreateTable();
    }
}
