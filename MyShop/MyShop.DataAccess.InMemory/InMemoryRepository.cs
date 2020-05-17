
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T: BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> Items;
        string ClassName;
        
        public InMemoryRepository()
        {
            ClassName = typeof(T).Name;
            Items = cache[ClassName] as List<T>;
            if (Items == null)
            {
                Items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[ClassName] = Items;
        }
        public void Insert(T t)
        {
            Items.Add(t);
        }

        public void DoSomething()
        {

        }

        public void Update(T t)
        {
            T tToUpdate = Items.FirstOrDefault(item => item.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            throw new Exception(ClassName +"Not Found");
        }

        public T Find(string Id)
        {
            T t = Items.FirstOrDefault(item => item.Id == Id);
            if (t != null)
            {
                return t;
            }
            throw new Exception(ClassName + "Not Found");
        }
        public IQueryable<T> Collection()
        {
            return Items.AsQueryable();
        }
        public void Delete(string Id)
        {
            T t = Items.FirstOrDefault(item => item.Id == Id);
            if (t != null)
            {
                Items.Remove(t);
            }
            throw new Exception(ClassName + "Not Found");
        }
        
    }
}
