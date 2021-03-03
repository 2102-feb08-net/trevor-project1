using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using DAL.Repositories;

namespace StoreApp.WebUI
{
    public class Dependencies
    {
        private string _connectionString;

        public Dependencies()
        {
            _connectionString = File.ReadAllText("C:/revature/project0-connection-string.txt");
        }

        public CustomerRepository GetCustomerRepository()
        {
            return new CustomerRepository(_connectionString);
        }

        public OrderRepository GetOrderRepository()
        {
            return new OrderRepository(_connectionString);
        }

        public ProductRepository GetProductRepository()
        {
            return new ProductRepository(_connectionString);
        }

        public StoreRepository GetStoreRepository()
        {
            return new StoreRepository(_connectionString);
        }
    }
}
