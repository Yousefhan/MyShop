using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Customer : BaseEntity
    {
        public string UserId { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Street { set; get; }
        public string City { set; get; }
        public string State { set; get; }
        public string ZipCode { set; get; }



    }
}
