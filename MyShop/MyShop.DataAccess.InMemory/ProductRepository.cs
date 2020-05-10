using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;
namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["Products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["Products"] = products;
        }

        public void Insert(Product P)
        {
            products.Add(P);
        }
        public void Update(Product product)
        {
           Product ProductToUpdate =  products.First(item => item.Id == product.Id);
           if (ProductToUpdate != null)
            {
                ProductToUpdate = product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
        public Product Find(string Id)
        {
            Product product = products.First(item => item.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product product = products.First(item => item.Id == Id);
            if (product != null)
            {
                products.Remove(product);
            }
            else
            {
                throw new Exception("Product Not Found");
            }
        }
    }
}
