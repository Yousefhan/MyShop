using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string Id { set; get; }
        public string Category { set; get; }
        public ProductCategory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
