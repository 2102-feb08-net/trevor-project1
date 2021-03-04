﻿using Microsoft.AspNetCore.Http;
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
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("api/orders")] //Not sure if I'll ever use this method based on my implementation
        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        [HttpGet("api/ordersByStore")]
        public IEnumerable<Order> GetOrderByStore(int storeID)
        {
            return _orderRepository.GetOrdersByStore(storeID);
        }

        [HttpGet("api/ordersByCustomer")]
        public IEnumerable<Order> GetOrderByCustomer(int customerID)
        {
            return _orderRepository.GetOrdersByCustomer(customerID);
        }

        [HttpGet("api/orders/{id}")]
        public Order GetOrderByID(int id)
        {
            return _orderRepository.GetOrderByID(id);
        }

        [HttpPost("api/orderAdd")]
        public void AddOrder(Order order)
        {
            _orderRepository.AddOrder(order);
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
