using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Core.Enums;
using CutomerTeller.WebAPIApp.Model;
using CutomerTeller.WebAPIApp.Model.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Controllers.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// Registers the account.
        /// </summary>
        /// <param name="accountModel">The account model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-account")]
        public IActionResult RegisterAccount(AccountModel accountModel)
        {
            try
            {
                var model = _accountService.RegisterAccount(accountModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets the available account by customer identifier.
        /// </summary>
        /// <param name="CustomerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpGet("get-accountbycustomerid/{customerId}")]
        public IActionResult GetAvailableAccountByCustomerId(string customerId)
        {
            try
            {
                var model = _accountService.GetAvailableAccountByCustomerId(Convert.ToInt32(customerId));
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
        /// Gets the AccountList.
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-account-list")]
        public IActionResult GetAccountList()
        {
            try
            {
                var model = _accountService.GetAccountList();
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
        /// Update Account
        /// </summary>
        /// <returns></returns>
        [HttpPut("update-account")]
        public IActionResult UpdateAccount(AccountModel accountModel)
        {
            try
            {
                var result = _accountService.UpdateAccount(accountModel);
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
        /// Delete Account
        /// </summary>
        /// <returns></returns>

        [HttpDelete("delete-account/{id}")]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                var result = _accountService.DeleteAccount(id);
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
        /// 1. Input Customer ID; able to retrieve account balances for all accounts for that customer
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-account-balance-for-customer/{customerId}")]
        public IActionResult GetAccountsBalanceForCustomer(string customerId)
        {
            try
            {
                var result = _accountService.GetAccountsBalanceForCustomer(Convert.ToInt32(customerId));
                if (result == null)
                    return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 2. Input customer ID, Account able to retrieve account balances for that account
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-account-balance/{customerId}/{accountId}")]
        public IActionResult GetAccountBalance(string customerId, string accountId)
        {
            try
            {
                var result = _accountService.GetAccountBalance(Convert.ToInt32(customerId), Convert.ToInt32(accountId));
                if (result == null)
                    return BadRequest();
                return Ok(result);
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
        [HttpPost("account-list")]
        public async Task<ActionResult> GetAccountList([FromBody] AccountFilter accountFilter)
        {
            try
            {
               var model = await _accountService.GetAccountList(accountFilter);
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

    }
}
