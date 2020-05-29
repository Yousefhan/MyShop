using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class BasketItem : BaseEntity
    {
        public string BasketId { set; get; }
        public string ProductId { set; get; }
        public int Quantity { set; get; }
    }
}
