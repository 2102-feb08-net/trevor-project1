using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Get an order by ID number
        /// </summary>
        /// <param name="id">Order ID</param>
        /// <returns>Order Model</returns>
        Order GetOrderByID(int id);

        /// <summary>
        /// Get a list of all orders in the database
        /// </summary>
        /// <returns>List of all orders</returns>
        List<Order> GetAllOrders();

        /// <summary>
        /// Get a list of all orders in the database by store
        /// </summary>
        /// <param name="store">Store to grab orders for</param>
        /// <returns>List of orders associated with store parameter</returns>
        List<Order> GetOrdersByStore(Store store);

        /// <summary>
        /// Get a list of all orders in the database by customer
        /// </summary>
        /// <param name="customer">Customer to grab orders for</param>
        /// <returns>List of orders associated with customer parameter</returns>
        List<Order> GetOrdersByCustomer(Customer customer);

        /// <summary>
        /// Add a new order to the database
        /// </summary>
        /// <param name="order">Order to add</param>
        /// <returns>New order ID</returns>
        int AddOrder(Order order);

        /// <summary>
        /// Update an order in the database
        /// </summary>
        /// <param name="order">Order to update</param>
        void UpdateOrder(Order order);

        /// <summary>
        /// Delete an order from the database
        /// </summary>
        /// <param name="order">Order to delete</param>
        void DeleteOrder(Order order);
    }
}
