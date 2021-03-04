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
    public class CustomerRepository : ICustomerRepository
    {
        private Project1Context _context;


        public CustomerRepository(Project1Context context)
        {
            _context = context;
        }

        public int AddCustomer(Customer customer)
        {
            CustomerDAL newCustomer = new CustomerDAL
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Address = customer.Address
            };
            _context.Add(newCustomer);
            _context.SaveChanges();
            return newCustomer.Id;
        }

        public void DeleteCustomer(Customer customer)
        {
            var query = _context.Customers.Find(customer.ID);
            if (query != null)
            {
                _context.Remove(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find customer to delete");
            }
        }

        public Customer GetCustomerByID(int id)
        {
            var query = _context.Customers.Find(id);
            if (query != null)
            {
                return new Customer
                {
                    ID = query.Id,
                    FirstName = query.FirstName,
                    LastName = query.LastName,
                    Email = query.Email,
                    Address = query.Address
                };
            }
            else
            {
                throw new Exception("Could not find customer with that ID");
            }
        }

        public List<Customer> GetCustomers(string firstName = null, string lastName = null)
        {
            IQueryable<CustomerDAL> query = _context.Customers;
            if (firstName != null && lastName != null)
            {
                query = query.Where(c => c.FirstName.Contains(firstName) && c.LastName.Contains(lastName));
            }
            else if (firstName != null)
            {
                query = query.Where(c => c.FirstName.Contains(firstName));
            }
            else if (lastName != null)
            {
                query = query.Where(c => c.LastName.Contains(lastName));
            }
            return query.Select(c => new Customer
            {
                ID = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                Address = c.Address
            }).ToList();
        }

        public void UpdateCustomer(Customer customer)
        {
            var query = _context.Customers.Find(customer.ID);
            if (query != null)
            {
                query.FirstName = customer.FirstName;
                query.LastName = customer.LastName;
                query.Email = customer.Email;
                query.Address = customer.Address;
                _context.Update(query);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Couldn't find customer with that ID");
            }
        }
    }
}
