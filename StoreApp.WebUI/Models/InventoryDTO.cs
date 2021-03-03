using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebUI.Models
{
    public class InventoryDTO
    {
        public int StoreID { get; set; }
        public List<ProductDTO> inventory { get; set; }


        public InventoryDTO()
        {
        }

        public InventoryDTO(int storeID)
        {
            StoreID = storeID;
            inventory = new List<ProductDTO>();
        }
    }
}
