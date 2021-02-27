using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbContextOptions<Project1Context> _options;


        public ProductRepository(string connectionString)
        {
            _options = new DbContextOptionsBuilder<Project1Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public int AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Product GetProductByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(string name = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
