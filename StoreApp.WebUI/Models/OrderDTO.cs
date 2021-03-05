using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebUI.Models
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public decimal TotalPrice { get; set; }
        public string CustomerName { get; set; }
        public int StoreID { get; set; }
        public DateTime OrderTime { get; set; }

        public OrderDTO()
        {
        }

    }
}
