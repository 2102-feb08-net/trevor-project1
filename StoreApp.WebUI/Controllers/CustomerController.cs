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
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("api/customers")]
        public IEnumerable<Customer> GetCustomers(string firstName = null, string lastName = null)
        {
            return _customerRepository.GetCustomers(firstName, lastName);
        }

        [HttpGet("api/customers/{id}")]
        public Customer GetCustomerByID(int id)
        {
            return _customerRepository.GetCustomerByID(id);
        }

        [HttpPost("api/customerAdd")]
        public void AddCustomer(CustomerDTO customer)
        {
            Customer toAdd = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address
            };
            _customerRepository.AddCustomer(toAdd);
        }

        [HttpPost("api/customerDelete")]
        public void DeleteCustomer(Customer customer)
        {
            _customerRepository.DeleteCustomer(customer);
        }

        [HttpPost("api/customerUpdate")]
        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.UpdateCustomer(customer);
        }
    }
}
