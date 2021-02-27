using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Order
    {
        private decimal _totalPrice;

        public Dictionary<Product, int> Cart { get; set; }

        public int ID { get; set; }
        public decimal TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Price on order cannot be negative");
                }
                else
                {
                    _totalPrice = value;
                }
            }
        }
        public Customer Customer { get; set; }
        public Store Store { get; set; }
        public DateTime OrderTime { get; set; }

        public Order()
        {
        }

        public Order(Customer customer, Store store)
        {
            Cart = new Dictionary<Product, int>();
            TotalPrice = 0.0M;
            Customer = customer;
            Store = store;
        }

        public void AddNewItemToCart(Product product, int quantity)
        {
            if (Cart.ContainsKey(product))
            {
                throw new ArgumentException("Item already exists in the inventory");
            }
            else if (quantity <= 0)
            {
                throw new ArgumentException("Can't add new item with 0 or less than 0 quantity to inventory");
            }
            else if (!Store.Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Can't add item to cart because it doesn't exist in store inventory");
            }
            else if(quantity > Store.Inventory[product])
            {
                throw new ArgumentException("Can't add item to cart, not enough inventory available");
            }
            else
            {
                Cart[product] = quantity;
                Store.Inventory[product] -= quantity;
            }
        }

        public void AddQuantityToCart(Product product, int quantity)
        {
            if (!Cart.ContainsKey(product))
            {
                throw new ArgumentException("Can't add quantity because item doesn't exist in inventory");
            }
            else if (quantity <= 0)
            {
                throw new ArgumentException("Can't add 0 or less than 0 quantity to item in inventory");
            }
            else if (!Store.Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Can't add quantity for item in cart because it doesn't exist in the store");
            }
            else if(quantity > Store.Inventory[product])
            {
                throw new ArgumentException("Can't add quantity for item in cart because not enough store inventory available");
            }
            else
            {
                Cart[product] += quantity;
                Store.Inventory[product] -= quantity;
            }
        }

        public void RemoveQuantityFromCart(Product product, int quantity)
        {
            if (!Cart.ContainsKey(product))
            {
                throw new ArgumentException("Can't remove quantity because item doesn't exist in inventory");
            }
            else if (quantity <= 0)
            {
                throw new ArgumentException("Can't remove 0 or less than 0 quantity from item in inventory");
            }
            else if (quantity > Cart[product])
            {
                throw new ArgumentException("Can't remove more than what already exists for item in inventory");
            }
            else
            {
                Cart[product] -= quantity;
                Store.Inventory[product] += quantity;
            }
        }

        public void DeleteProductFromCart(Product product)
        {
            if (!Cart.ContainsKey(product))
            {
                throw new ArgumentException("Can't delete product from inventory because it doesn't exist");
            }
            else
            {
                Store.Inventory[product] += Cart[product];
                Cart.Remove(product);
            }
        }

        public void SubmitOrder()
        {
            foreach(var item in Cart)
            {
                TotalPrice += item.Key.Price * item.Value;
            }
            Store.GrossProfit += TotalPrice;
            OrderTime = DateTime.Now;
        }
    }
}
