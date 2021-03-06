using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebUI.Models
{
    public class NewOrderDTO
    {
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public List<ProductDTO> Items { get; set; }

        public NewOrderDTO()
        {
        }
    }
}
