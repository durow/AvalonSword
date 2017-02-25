using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayx.AvalonSword
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T GetOne(string keyField, object value);
        void Inser(IEnumerable<T> entityList);
        void Update(T entity, string keyField, object value);
        void UpdateField(string keyField, object keyValue, string updateField, object updateValue);
        void DeleteOne(string key, object value);
    }
}
