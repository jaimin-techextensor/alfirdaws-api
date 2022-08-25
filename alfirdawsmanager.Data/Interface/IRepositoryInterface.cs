using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alfirdawsmanager.Data.Interface
{
    public interface IRepositoryInterface<T> : IDisposable where T : class
    {
        IEnumerable<T> SelectAll();
        T SelectByID(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        void Save();
        void InsertOrUpdate(T obj, int id);
    }
}
