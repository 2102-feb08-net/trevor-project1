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

    }
}
