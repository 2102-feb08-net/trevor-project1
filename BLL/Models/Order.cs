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

        public Dictionary<Product, int> Items { get; set; }

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
            Items = new Dictionary<Product, int>();
            TotalPrice = 0.0M;
            Customer = customer;
            Store = store;
        }
    }
}
