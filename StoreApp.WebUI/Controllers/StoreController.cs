using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Models;
using DAL.Repositories;

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
        public IEnumerable<Store> GetStores(string state = null, string city = null)
        {
            return _storeRepository.GetStores(state, city);
        }

        [HttpGet("api/stores/{id}")]
        public Store GetStoreByID(int id)
        {
            return _storeRepository.GetStoreByID(id);
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
