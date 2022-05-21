using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Model.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Description;

namespace CutomerTeller.WebAPIApp.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Registers the customer.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-customer")]
        public IActionResult RegisterCustomer(CustomerModel customerModel)
        {
            try
            {
                var model = _customerService.RegisterCustomer(customerModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the customer list.
        /// </summary>
        /// <returns></returns>
        [HttpPost("customer-list")]
        public async Task<ActionResult> GetCustomerList([FromBody] CustomerFilter customerFilter)
        {
            try
            {
                var model = await _customerService.GetCustomerList(customerFilter);
                if (model == null)
                    return BadRequest();
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// update customer
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-customer")]
        public IActionResult UpdateCustomer(CustomerModel customerModel)
        {
            try
            {
                var result = _customerService.UpdateCustomer(customerModel);
                if (!result)
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }            
        }

        /// <summary>
        /// delete customer.
        /// </summary>
        /// <returns></returns>

        [HttpDelete("delete-customer/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var result = _customerService.DeleteCustomer(id);
                if (!result)
                    return BadRequest();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }


    }
}
