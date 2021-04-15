using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class CartItem : EntityBase
    {
        public virtual Product Product { get; set; }

        public double EnteredDiscount { get; set; }

        public double FinalPrice { get; set; }
    }
}
