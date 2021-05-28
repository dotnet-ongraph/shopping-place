using Fulfillment.Core.Entities;
using Fulfillment.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FulfillmentApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Private Fields

        private readonly CustomerService _customerService;

        #endregion

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        #region Public Methods

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("customers")]
        public List<Customer> GetAllCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                return customers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <returns></returns>
        [HttpPost("create")]
        public IActionResult Create([FromBody] Customer customer)
        {
            try
            {
                return Ok(_customerService.CreateCustomer(customer));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">CustomerId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(_customerService.GetCustomer(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Get customer by email
        /// </summary>
        /// <param name="emailId">email id</param>
        /// <returns><category/returns>
        [HttpGet]
        [Route("{emailId}")]
        public IActionResult GetCategoryByEmailId(string emailId)
        {
            if (string.IsNullOrWhiteSpace(emailId))
            {
                throw new ArgumentException("Email Id cannot be empty or null");
            }
            try
            {
                var data = _customerService.GetCustomerByEmail(emailId);
                if (data == null)
                    return NoContent();
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        #endregion
    }
}
