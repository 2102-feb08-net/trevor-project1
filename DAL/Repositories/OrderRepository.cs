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
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContextOptions<Project1Context> _options;


        public OrderRepository(string connectionString)
        {
            _options = new DbContextOptionsBuilder<Project1Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public int AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByStore(Store store)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
