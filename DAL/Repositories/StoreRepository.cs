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
        private readonly DbContextOptions<Project1Context> _options;


        public StoreRepository(string connectionString)
        {
            _options = new DbContextOptionsBuilder<Project1Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public int AddStore(Store store)
        {
            throw new NotImplementedException();
        }

        public void DeleteStore(Store store)
        {
            throw new NotImplementedException();
        }

        public Store GetStoreByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<Store> GetStores(string state = null, string city = null)
        {
            throw new NotImplementedException();
        }

        public void UpdateStore(Store store)
        {
            throw new NotImplementedException();
        }
    }
}
