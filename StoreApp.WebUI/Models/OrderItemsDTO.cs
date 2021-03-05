using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebUI.Models
{
    public class OrderItemsDTO
    {
        public int OrderID { get; set; }
        public List<ProductDTO> Items { get; set; }
        public OrderItemsDTO()
        {
        }
    }
}
