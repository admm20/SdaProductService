using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SdaProductService.Models
{
    public class ProductAdd
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Iban { get; set; }
    }
}
