using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Product : EntityBase
    {

        public string Name { get; set; }

        public double Price { get; set; }

        public double MaxDiscount { get; set; }
    }
}
