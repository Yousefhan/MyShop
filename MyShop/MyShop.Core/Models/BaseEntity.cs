using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { set; get; }
        public DateTimeOffset CreatedAt { set; get; }


        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTimeOffset.Now;
        }
    }
}
