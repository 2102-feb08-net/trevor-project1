﻿using Microsoft.AspNetCore.Http;
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

        private readonly ProductRepository _productRepository;

        public StoreController(StoreRepository storeRepository, ProductRepository productRepository)
        {
            _storeRepository = storeRepository;
            _productRepository = productRepository;
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

        [HttpGet("api/storeInventory/{id}")]
        public List<ProductDTO> GetStoreInventory(int id)
        {
            var store = _storeRepository.GetStoreByID(id);
            List<ProductDTO> inventory = new List<ProductDTO>();
            foreach(var item in store.Inventory)
            {
                inventory.Add(new ProductDTO
                {
                    ID = item.Key.ID,
                    Name = item.Key.Name,
                    Price = item.Key.Price,
                    Quantity = item.Value
                });
            }
            return inventory;
        }

        [HttpPost("api/storeInventoryAddItem/{id}")]
        public void AddInventoryItem(int id, string productName, decimal productPrice, int quantity)
        {
            Store store = _storeRepository.GetStoreByID(id);
            Product p = new Product
            {
                Name = productName,
                Price = productPrice
            };
            p.ID = _productRepository.AddProduct(p);
            store.AddNewItemToInventory(p, quantity);
            _storeRepository.UpdateStore(store);
        }

        [HttpPost("api/storeInventoryEditItem/{id}")]
        public void EditInventoryItemQuantity(int id, int productID, int newQuantity)
        {
            Store store = _storeRepository.GetStoreByID(id);
            Product p = _productRepository.GetProductByID(productID);
            store.EditQuantityToItem(p, newQuantity);
            _storeRepository.UpdateStore(store);
        }

        [HttpPost("api/storeAdd")]
        public void AddStore(StoreDTO store)
        {
            Store newStore = new Store
            {
                Name = store.Name,
                City = store.City,
                State = store.State,
                GrossProfit = 0
            };
            _storeRepository.AddStore(newStore);
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
