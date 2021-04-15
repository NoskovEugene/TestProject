using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class ProductCartDto : EntityBaseDto
    {
        public List<CartItemDto> CartItems { get; set; }

        public double TotalSum { get; set; }

        public double TotalSumWithDiscount { get; set; }
    }
}
