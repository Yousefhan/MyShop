using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class OrderItem : BaseEntity
    {
        public string OrderId { set; get; }
        public string ProductId { set; get; }
        public string ProductName { set; get; }
        public decimal Price { set; get; }
        public string Image { set; get; }
        public int Quantity { set; get; }
    }
}
