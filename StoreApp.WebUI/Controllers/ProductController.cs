using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories;

namespace StoreApp.WebUI.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("api/products")]
        public IEnumerable<Product> GetProducts(string name = null)
        {
            return _productRepository.GetProducts(name);
        }

        [HttpGet("api/products/{id}")]
        public Product GetProductByID(int id)
        {
            return _productRepository.GetProductByID(id);
        }

        [HttpPost("api/productAdd")]
        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        [HttpPost("api/productDelete")]
        public void DeleteProduct(Product product)
        {
            _productRepository.DeleteProduct(product);
        }

        [HttpPost("api/productUpdate")]
        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
        }
    }
}
