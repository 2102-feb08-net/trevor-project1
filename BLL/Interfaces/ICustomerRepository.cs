using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL.Interfaces
{
    interface ICustomerRepository
    {
        /// <summary>
        /// Get a customer by their ID
        /// </summary>
        /// <param name="id">Customer ID</param>
        /// <returns>Customer Model</returns>
        Customer GetCustomerByID(int id);

        /// <summary>
        /// Get a list of customers with the option of searching for customers by first and/or last name
        /// </summary>
        /// <param name="firstName">Optional search parameter</param>
        /// <param name="lastName">Optional search parameter</param>
        /// <returns>List of customer models found by search</returns>
        List<Customer> GetCustomers(string firstName = null, string lastName = null);

        /// <summary>
        /// Add a new customer to the database
        /// </summary>
        /// <param name="customer">Customer to add</param>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Update a customer in the database
        /// </summary>
        /// <param name="customer">Customer to update</param>
        void UpdateCustomer(Customer customer);

        /// <summary>
        /// Delete a customer from the database
        /// </summary>
        /// <param name="customer">Customer to delete</param>
        void DeleteCustomer(Customer customer);
    }
}
