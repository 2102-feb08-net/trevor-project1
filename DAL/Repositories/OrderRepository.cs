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
        private Project1Context _context;


        public OrderRepository(Project1Context context)
        {
            _context = context;
        }

        public int AddOrder(Order order)
        {
            OrderDAL newOrder = new OrderDAL
            {
                StoreId = order.Store.ID,
                CustomerId = order.Customer.ID,
                TotalPrice = order.TotalPrice,
                OrderTime = order.OrderTime,
            };
            _context.Add(newOrder);
            _context.SaveChanges();
            return newOrder.Id;
        }

        public void DeleteOrder(Order order)
        {
            var query = _context.Orders.Find(order.ID);
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

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Order GetOrderByID(int id)
        {
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .First(o => o.Id == id);
            if (query != null)
            {
                var inventory = query.OrderItems.Select(
                    x => new KeyValuePair<Product, int>(
                    new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList();

                return new Order
                {
                    ID = query.Id,
                    Store = new Store
                    {
                        ID = query.Store.Id,
                        Name = query.Store.Name,
                        City = query.Store.City,
                        State = query.Store.State,
                        GrossProfit = query.Store.Profit,
                        Inventory = query.Store.StoreItems.Select(
                                x => new KeyValuePair<Product, int>(
                                new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList().ToDictionary(x => x.Key, y => y.Value)
                    },
                    Customer = new Customer
                    {
                        ID = query.Customer.Id,
                        FirstName = query.Customer.FirstName,
                        LastName = query.Customer.LastName,
                        Email = query.Customer.Email,
                        Address = query.Customer.Address
                    },
                    TotalPrice = query.TotalPrice,
                    OrderTime = query.OrderTime,
                    Cart = inventory.ToDictionary(x => x.Key, y => y.Value)
                };
            }
            else
            {
                throw new Exception("Couldn't find order with that ID");
            }
        }

        public List<Order> GetOrdersByCustomer(int customerID)
        {
            List<Order> orders = new List<Order>();
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Where(o => o.CustomerId == customerID).ToList();
            if (query != null)
            {
                foreach (var order in query)
                {
                    var items = order.OrderItems.Select(
                    x => new KeyValuePair<Product, int>(
                    new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList();
                    orders.Add(new Order
                    {
                        ID = order.Id,
                        Store = new Store
                        {
                            ID = order.Store.Id,
                            Name = order.Store.Name,
                            City = order.Store.City,
                            State = order.Store.State,
                            GrossProfit = order.Store.Profit,
                            Inventory = order.Store.StoreItems.Select(
                                x => new KeyValuePair<Product, int>(
                                new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList().ToDictionary(x => x.Key, y => y.Value)
                        },
                        Customer = new Customer
                        {
                            ID = order.Customer.Id,
                            FirstName = order.Customer.FirstName,
                            LastName = order.Customer.LastName,
                            Email = order.Customer.Email,
                            Address = order.Customer.Address
                        },
                        TotalPrice = order.TotalPrice,
                        OrderTime = order.OrderTime,
                        Cart = items.ToDictionary(x => x.Key, y => y.Value)
                    });
                }
                return orders;
            }
            else
            {
                throw new Exception("Couldn't find any orders for customer with that ID");
            }
        }

        public List<Order> GetOrdersByStore(int storeID)
        {
            List<Order> orders = new List<Order>();
            var query = _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Store)
                .Include(o => o.Customer)
                .Where(o => o.StoreId == storeID).ToList();
            if (query != null)
            {
                foreach (var order in query)
                {
                    var items = order.OrderItems.Select(
                    x => new KeyValuePair<Product, int>(
                    new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList();
                    orders.Add(new Order
                    {
                        ID = order.Id,
                        Store = new Store
                        {
                            ID = order.Store.Id,
                            Name = order.Store.Name,
                            City = order.Store.City,
                            State = order.Store.State,
                            GrossProfit = order.Store.Profit,
                            Inventory = order.Store.StoreItems.Select(
                                x => new KeyValuePair<Product, int>(
                                new Product(x.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList().ToDictionary(x => x.Key, y => y.Value)
                        },
                        Customer = new Customer
                        {
                            ID = order.Customer.Id,
                            FirstName = order.Customer.FirstName,
                            LastName = order.Customer.LastName,
                            Email = order.Customer.Email,
                            Address = order.Customer.Address
                        },
                        TotalPrice = order.TotalPrice,
                        OrderTime = order.OrderTime,
                        Cart = items.ToDictionary(x => x.Key, y => y.Value)
                    });
                }
                return orders;
            }
            else
            {
                throw new Exception("Couldn't find any orders for customer with that ID");
            }
        }

        public void UpdateOrder(Order order)
        {
            OrderDAL toUpdate = _context.Orders.Find(order.ID);
            if (toUpdate != null)
            {
                toUpdate.TotalPrice = order.TotalPrice;
                toUpdate.OrderTime = order.OrderTime;
                toUpdate.OrderItems = order.Cart.Select(kv => new OrderItemDAL
                {
                    OrderId = order.ID,
                    ProductId = kv.Key.ID,
                    Quantity = kv.Value
                }).ToList();
                _context.Update(toUpdate);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find order to update");
            }
        }
    }
}
