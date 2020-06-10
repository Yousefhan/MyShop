using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
        public string FirstName { set; get; }
        public string Surename { set; get; }
        public string Email { set; get; }
        public string Street { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string ZipCode { set; get; }
        public string OrderStatus { set; get; }
        public virtual ICollection<OrderItem> OrderItems {set;get;}
    }
}
