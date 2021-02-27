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

        public void AddQuantityToItem(Product product, int quantity)
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
                Inventory[product] += quantity;
            }
        }

        public void RemoveQuantityFromItem(Product product, int quantity)
        {
            if (!Inventory.ContainsKey(product))
            {
                throw new ArgumentException("Can't remove quantity because item doesn't exist in inventory");
            }
            else if (quantity <= 0)
            {
                throw new ArgumentException("Can't remove 0 or less than 0 quantity from item in inventory");
            }
            else if(quantity > Inventory[product])
            {
                throw new ArgumentException("Can't remove more than what already exists for item in inventory");
            }
            else
            {
                Inventory[product] -= quantity;
            }
        }

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
