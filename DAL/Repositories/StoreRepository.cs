using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly Project1Context _context;


        public StoreRepository(Project1Context context)
        {
            _context = context;
        }

        public int AddStore(Store store)
        {
            var newStore = new StoreDAL
            {
                Name = store.Name,
                City = store.City,
                State = store.State,
                Profit = store.GrossProfit,
            };
            _context.Add(newStore);
            _context.SaveChanges();
            return newStore.Id;
        }

        public void DeleteStore(Store store)
        {
            var query = _context.Stores.Find(store.ID);
            if (query != null)
            {
                _context.Remove(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find product to delete");
            }
        }

        public Store GetStoreByID(int id)
        {
            StoreDAL query = _context.Stores
                .Include(s => s.StoreItems)
                    .ThenInclude(p => p.Product)
                 .First(s => s.Id == id);

            if (query != null)
            {
                var inventory = query.StoreItems.Select(
                    x => new KeyValuePair<Product, int>(
                    new Product(x.Product.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList();

                return new Store
                {
                    ID = query.Id,
                    Name = query.Name,
                    City = query.City,
                    State = query.State,
                    GrossProfit = query.Profit,
                    Inventory = inventory.ToDictionary(x => x.Key, y => y.Value)
                };
            }
            else
            {
                throw new Exception("Could not locate store with this ID");
            }
        }

        public List<Store> GetStores(string state = null, string city = null)
        {
            List<Store> stores = new List<Store>();
            IQueryable<StoreDAL> query = _context.Stores
                .Include(s => s.StoreItems)
                    .ThenInclude(p => p.Product);
            if (state != null && city != null)
            {
                query = query.Where(x => x.State.Contains(state) && x.City.Contains(city));
            }
            else if(state != null)
            {
                query = query.Where(x => x.State.Contains(state));
            }
            else if(city != null)
            {
                query = query.Where(x => x.State.Contains(city));
            }
            foreach (var store in query)
            {
                var inventory = store.StoreItems.Select(
                    x => new KeyValuePair<Product, int>(
                    new Product(x.Product.Id, x.Product.Name, x.Product.Price), x.Quantity)).ToList();
                stores.Add(new Store
                {
                    ID = store.Id,
                    Name = store.Name,
                    City = store.City,
                    State = store.State,
                    GrossProfit = store.Profit,
                    Inventory = inventory.ToDictionary(x => x.Key, y => y.Value)
                });
            }
            return stores;
        }

        public void UpdateStore(Store store)
        {
            var query = _context.Stores
                .Include(s => s.StoreItems)
                    .ThenInclude(p => p.Product).First(s => s.Id == store.ID);
            if (query != null)
            {
                query.Name = store.Name;
                query.City = store.City;
                query.State = store.State;
                query.Profit = store.GrossProfit;
                query.StoreItems = store.Inventory.Select(kv => new StoreItemDAL
                {
                    StoreId = store.ID,
                    ProductId = kv.Key.ID,
                    Quantity = kv.Value,
                }).ToList();
                _context.Update(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Could not locate store to update");
            }
        }
    }
}
