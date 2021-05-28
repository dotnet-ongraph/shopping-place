using Core.Interfaces;
using Fulfillment.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Fulfillment.Core.Services
{
    public class CustomerService
    {
        private readonly IEntityRepository<Customer> _customerRepository;

        public CustomerService(IEntityRepository<Customer> repository)
        {
            _customerRepository = repository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll().ToList();
        }

        public Customer CreateCustomer(Customer customer)
        {
            _customerRepository.Insert(customer);
            _customerRepository.SaveChanges();
            return customer;
        }

        public Customer GetCustomer(int id)
        {
            return _customerRepository.Find(c => c.IsDeleted == false && c.Id == id).SingleOrDefault();
        }

        public Customer GetCustomerByEmail(string emailId)
        {
            return _customerRepository.Find(c => c.IsDeleted == false && c.EmailId.ToLower().Equals(emailId.ToLower())).SingleOrDefault();
        }
    }
}
