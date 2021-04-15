using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos
{
    public class ProductDto : EntityBaseDto
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public double MaxDiscount { get; set; }
    }
}
