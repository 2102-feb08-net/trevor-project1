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
            using var _context = new Project1Context(_options);
            ProductDAL newProduct = new ProductDAL
            {
                Name = product.Name,
                Price = product.Price
            };
            _context.Add(newProduct);
            _context.SaveChanges();
            return newProduct.Id;
        }

        public void DeleteProduct(Product product)
        {
            using var _context = new Project1Context(_options);
            var query = _context.Products.Find(product.ID);
            if (query != null)
            {
                _context.Remove(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find product to delete");
            }
        }

        public Product GetProductByID(int id)
        {
            using var _context = new Project1Context(_options);
            var query = _context.Products.Find(id);
            if (query != null)
            {
                return new Product
                {
                    ID = query.Id,
                    Name = query.Name,
                    Price = query.Price
                };
            }
            else
            {
                throw new Exception("Couldn't find product with that ID");
            }
        }

        public List<Product> GetProducts(string name = null)
        {
            using var _context = new Project1Context(_options);
            IQueryable<ProductDAL> query = _context.Products;
            if (name != null)
            {
                query = query.Where(p => p.Name.Contains(name));
            }
            if (query != null)
            {
                return query.Select(p => new Product
                {
                    ID = p.Id,
                    Name = p.Name,
                    Price = p.Price
                }).ToList();
            }
            else
            {
                throw new Exception("No products exist");
            }
        }

        public void UpdateProduct(Product product)
        {
            using var _context = new Project1Context(_options);
            var query = _context.Products.Find(product.ID);
            if (query != null)
            {
                query.Name = product.Name;
                query.Price = product.Price;
                _context.Update(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find product to update");
            }
        }
    }
}
