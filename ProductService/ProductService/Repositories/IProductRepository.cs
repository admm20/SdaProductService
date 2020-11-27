using SdaProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SdaProductService.Repositories
{
    public interface IProductRepository
    {
        Product Get(int id);
        IEnumerable<Product> GetAll();
        Product Update(int id, ProductAdd product);
        Product Add(ProductAdd product);
        bool Delete(int id);
    }
}
