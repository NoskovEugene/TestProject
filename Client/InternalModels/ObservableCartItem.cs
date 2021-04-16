using Common.Dtos;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.InternalModels
{
    public class ObservableCartItem : BindableBase
    {
        public int Id { get; set; }

        public ProductDto Product { get; set; }

        public double EnteredDiscount { get; set; }

        public double FinalPrice { get; set; }
    }
}
