using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class Store
    {
        private string _city;
        private string _state;

        public Dictionary<Product, int> Inventory { get; set; }

        public int ID { get; set; }
        public string Name { get; set; }
        public string City
        {
            get { return _city; }
            set
            {
                foreach (char c in value)
                {
                    if (!Char.IsLetter(c) && !Char.IsWhiteSpace(c))
                    {
                        throw new ArgumentException("Store city must contain only letters and spaces");
                    }
                }
                _city = value;
            }
        }
        public string State
        {
            get { return _state; }
            set
            {
                foreach (char c in value)
                {
                    if (!Char.IsLetter(c) && !Char.IsWhiteSpace(c))
                    {
                        throw new ArgumentException("Store state must contain only letters and spaces");
                    }
                }
                _state = value;
            }
        }
        public decimal GrossProfit { get; set; }

        public Store()
        {
        }

        public Store(string name, string city, string state)
        {
            Name = name;
            City = city;
            State = state;
            Inventory = new Dictionary<Product, int>();
            GrossProfit = 0.0M;
        }

        /// <summary>
        /// Adds a new item to store inventory that doesn't already exist
        /// </summary>
        /// <param name="product">New product</param>
        /// <param name="quantity">Amount to add</param>
        public void AddNewItemToInventory(Product product, int quantity)
        {
            if (Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Item already exists in the inventory");
            }
            else if(quantity <= 0)
            {
                throw new ArgumentException("Can't add new item with 0 or less than 0 quantity to inventory");
            }
            else
            {
                Inventory[product] = quantity;
            }
        }

        /// <summary>
        /// Edit's quantity for a product that already exists in store inventory
        /// </summary>
        /// <param name="product">Product to change quantity for</param>
        /// <param name="quantity">New quantity</param>
        public void EditQuantityToItem(Product product, int quantity)
        {
            if (!Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Can't add quantity because item doesn't exist in inventory");
            }
            else if(quantity <= 0)
            {
                throw new ArgumentException("Can't add 0 or less than 0 quantity to item in inventory");
            }
            else
            {
                Inventory[product] = quantity;
            }
        }

        /// <summary>
        /// Deletes a product entirely from store inventory
        /// </summary>
        /// <param name="product">Product to delete</param>
        public void DeleteProductFromInventory(Product product)
        {
            if (!Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Can't delete product from inventory because it doesn't exist");
            }
            else
            {
                Inventory.Remove(product);
            }
        }

    }
}
