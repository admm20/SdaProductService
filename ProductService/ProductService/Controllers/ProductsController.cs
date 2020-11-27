using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SdaProductService.Models;
using SdaProductService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SdaProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository repo)
        {
            _productRepository = repo;
        }

        // GET api/products
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return Ok(_productRepository.GetAll());
        }

        // GET api/products/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Product> Get(int id)
        {
            var product = _productRepository.Get(id);

            if (product != null)
                return Ok(product);

            return NotFound();
        }

        // POST api/products
        [HttpPost]
        [Authorize]
        public ActionResult<Product> Post([FromBody] ProductAdd product)
        {
            var addedProduct = _productRepository.Add(product);

            return Ok(addedProduct);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<Product> Put(int id, [FromBody] ProductAdd product)
        {
            return Ok(_productRepository.Update(id, product));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult Delete(int id)
        {
            var deleted = _productRepository.Delete(id);

            if (deleted)
                return Ok();

            return NotFound();
        }
    }
}
