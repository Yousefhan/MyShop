using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketItemViewModel
    {
        public string Id { set; get; }
        public int Quantity { set; get; }
        public string ProductName { set; get; }
        public decimal Price { set; get; }
        public string Image { set; get; }
    }
}
