using alfirdawsmanager.Data.Interface;
using alfirdawsmanager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace alfirdawsmanager.Data.Repository
{
    public class RepositoryPattern<T> : IRepositoryInterface<T> where T : class
    {
        private AlfirdawsManagerDbContext alFirdawsManagerEntities = null;
        private DbSet<T> table = null;
        public RepositoryPattern()
        {
            this.alFirdawsManagerEntities = new AlfirdawsManagerDbContext();
            table = alFirdawsManagerEntities.Set<T>();
        }

        public void Dispose()
        {

        }

        public void Delete(object id)
        {
            if(id != null && table != null)
            {
                T existing = table.Find(id);
                if (existing != null)
                {
                    table.Remove(existing);
                }
            }
            
        }

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            alFirdawsManagerEntities.SaveChanges();
        }

        public IEnumerable<T> SelectAll()
        {
            return table.ToList();
        }

        public IQueryable<T> SelcetAllIQueryable()
        {
            return table.AsQueryable();
        }

        public T SelectByID(object id)
        {
            return table.Find(id);
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            alFirdawsManagerEntities.Entry(obj).State = EntityState.Modified;
        }

        public void InsertOrUpdate(T entry, int id)
        {

            if (id.Equals(default(int)))
            {
                table.Add(entry);
                Save();
            }
            else
            {
                Update(entry);
            }
        }
    }
}
