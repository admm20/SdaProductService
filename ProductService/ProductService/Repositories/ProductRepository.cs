using SdaProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SdaProductService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private static int _ID = 0;
        private static int _AUTOINCREMENT_ID { get { return _ID++; } }

        public static List<Product> _PRODUCT_DB = new List<Product>();

        public Product Get(int id)
        {
            return _PRODUCT_DB.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _PRODUCT_DB;
        }

        public Product Add(ProductAdd product)
        {
            var res = new Product()
            {
                Id = _AUTOINCREMENT_ID,
                Color = product.Color,
                Iban = product.Iban,
                Name = product.Name,
                Price = product.Price
            };

            _PRODUCT_DB.Add(res);

            return res;
        }

        public bool Delete(int id)
        {
            var res = _PRODUCT_DB.SingleOrDefault(p => p.Id == id);

            if (res != null)
            {
                _PRODUCT_DB.Remove(res);
                return true;
            }

            return false;
        }

        public Product Update(int id, ProductAdd product)
        {
            var prod = _PRODUCT_DB.SingleOrDefault(p => p.Id == id);

            prod.Color = product.Color;
            prod.Iban = product.Iban;
            prod.Name = product.Name;
            prod.Price = product.Price;

            return prod;
        }
    }
}
