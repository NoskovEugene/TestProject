using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class ProductCart : EntityBase
    {
        public virtual List<CartItem> CartItems { get; set; }

        public double TotalSum { get; set; }

        public double TotalSumWithDiscount { get; set; }
    }
}
