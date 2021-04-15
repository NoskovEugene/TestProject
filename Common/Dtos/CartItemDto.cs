using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class CartItemDto : EntityBaseDto
    {
        public ProductDto ProductDto { get; set; }

        public double EnteredDiscount { get; set; }
    }
}
