using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class ProductParameters : RequestParameters
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; } = decimal.MaxValue;

        public bool ValidPriceRange => MinPrice <= MaxPrice;

    }
}
