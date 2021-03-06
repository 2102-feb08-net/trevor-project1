using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories;
using StoreApp.WebUI.Models;

namespace StoreApp.WebUI.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly StoreRepository _storeRepository;
        private readonly ProductRepository _productRepository;

        public OrderController(OrderRepository orderRepository, StoreRepository storeRepository, CustomerRepository customerRepository, ProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _storeRepository = storeRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }

        [HttpGet("api/orders")] //Not sure if I'll ever use this method based on my implementation
        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        [HttpGet("api/ordersByStore/{storeID}")]
        public IEnumerable<OrderDTO> GetOrderByStore(int storeID)
        {
            var orders = _orderRepository.GetOrdersByStore(storeID);
            List<OrderDTO> toReturn = new List<OrderDTO>();
            foreach(var order in orders)
            {
                toReturn.Add(new OrderDTO
                {
                    ID = order.ID,
                    TotalPrice = order.TotalPrice,
                    CustomerName = order.Customer.FirstName + " " + order.Customer.LastName,
                    StoreID = order.Store.ID,
                    OrderTime = order.OrderTime
                });
            }
            return toReturn;
        }

        [HttpGet("api/ordersByCustomer")]
        public IEnumerable<Order> GetOrderByCustomer(int customerID)
        {
            return _orderRepository.GetOrdersByCustomer(customerID);
        }

        [HttpGet("api/orders/{id}")]
        public List<ProductDTO> GetOrderByID(int id)
        {
            var order = _orderRepository.GetOrderByID(id);
            List<ProductDTO> items = new List<ProductDTO>();

            foreach(var item in order.Cart)
            {
                items.Add(new ProductDTO
                {
                    ID = item.Key.ID,
                    Name = item.Key.Name,
                    Price = item.Key.Price,
                    Quantity = item.Value
                });
            }
            return items;
        }

        [HttpPost("api/orderAdd")]
        public void AddOrder(int storeID, int customerID, List<ProductDTO> products)
        {
            Store store = _storeRepository.GetStoreByID(storeID);
            Customer customer = _customerRepository.GetCustomerByID(customerID);
            Order order = new Order(customer, store);
            foreach(var item in products)
            {
                Product p = _productRepository.GetProductByID((int)item.ID);
                order.AddNewItemToCart(p, (int)item.Quantity);
            }
            _orderRepository.AddOrder(order);
            _storeRepository.UpdateStore(order.Store);
        }

        [HttpPost("api/orderDelete")]
        public void DeleteOrder(Order order)
        {
            _orderRepository.DeleteOrder(order);
        }

        [HttpPost("api/orderUpdate")]
        public void UpdateOrder(Order order)
        {
            _orderRepository.UpdateOrder(order);
        }
    }
}
