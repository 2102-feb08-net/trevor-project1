using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    interface IProductRepository
    {
        /// <summary>
        /// Get a product by ID number
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product Model</returns>
        Product GetProductByID(int id);

        /// <summary>
        /// Get a list of all products with the option of searching by product name
        /// </summary>
        /// <param name="name">Optional parameter to search by name</param>
        /// <returns>List of products found by search</returns>
        List<Product> GetProducts(string name = null);

        /// <summary>
        /// Add a new product to database
        /// </summary>
        /// <param name="product">Product to add</param>
        void AddProduct(Product product);

        /// <summary>
        /// Update a product in the database
        /// </summary>
        /// <param name="product">Product to update</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Delete a product in the database
        /// </summary>
        /// <param name="product">Product to delete</param>
        void DeleteProduct(Product product);
    }
}
