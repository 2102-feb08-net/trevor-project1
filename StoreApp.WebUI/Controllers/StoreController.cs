using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories;
using StoreApp.WebUI.Models;

namespace StoreApp.WebUI.Controllers
{
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly StoreRepository _storeRepository;

        public Dependencies dependencies;

        public StoreController()
        {
            dependencies = new Dependencies();
            _storeRepository = dependencies.GetStoreRepository();
        }

        [HttpGet("api/stores")]
        public IEnumerable<StoreDTO> GetStores(string state = null, string city = null)
        {
            var stores = _storeRepository.GetStores(state, city);
            List<StoreDTO> storeDTOs = new List<StoreDTO>();
            foreach(var store in stores)
            {
                storeDTOs.Add(new StoreDTO
                {
                    ID = store.ID,
                    Name = store.Name,
                    City = store.City,
                    State = store.State,
                    GrossProfit = store.GrossProfit
                });
            }
            return storeDTOs;
        }

        [HttpGet("api/stores/{id}")]
        public StoreDTO GetStoreByID(int id)
        {
            var store = _storeRepository.GetStoreByID(id);
            return new StoreDTO
            {
                ID = store.ID,
                Name = store.Name,
                City = store.City,
                State = store.State,
                GrossProfit = store.GrossProfit
            };
        }

        [HttpPost("api/storeAdd")]
        public void AddStore(Store store)
        {
            _storeRepository.AddStore(store);
        }

        [HttpPost("api/storeDelete")]
        public void DeleteStore(Store store)
        {
            _storeRepository.DeleteStore(store);
        }

        [HttpPost("api/storeUpdate")]
        public void UpdateStore(Store store)
        {
            _storeRepository.UpdateStore(store);
        }
    }
}
