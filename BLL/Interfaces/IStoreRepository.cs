using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    public interface IStoreRepository
    {
        /// <summary>
        /// Get a store by ID number
        /// </summary>
        /// <param name="id">Store ID</param>
        /// <returns>Store model</returns>
        Store GetStoreByID(int id);

        /// <summary>
        /// Get a list of stores with the option of searching by state and/or city
        /// </summary>
        /// <param name="state">Optional parameter to search by state</param>
        /// <param name="city">Optional parameter to search by city</param>
        /// <returns>List of stores found by the search</returns>
        List<Store> GetStores(string state = null, string city = null);

        /// <summary>
        /// Add a new store to database including inventory
        /// </summary>
        /// <param name="store">Store to add</param>
        /// <returns>New store ID</returns>
        int AddStore(Store store);

        /// <summary>
        /// Update a store in the database
        /// </summary>
        /// <param name="store">Store to update</param>
        void UpdateStore(Store store);

        /// <summary>
        /// Delete a store from the database including inventory
        /// </summary>
        /// <param name="store">Store to delete</param>
        void DeleteStore(Store store);
    }
}
