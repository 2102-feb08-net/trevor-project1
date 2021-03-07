using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebUI.Models
{
    public class StoreDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal? GrossProfit { get; set; }

        public StoreDTO()
        {
        }

        public StoreDTO(int id, string name, string city, string state, decimal grossProfit)
        {
            ID = id;
            Name = name;
            City = city;
            State = state;
            GrossProfit = grossProfit;
        }
    }
}
