using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Model.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;
        public TransactionController(ITransactionService transactionService, ILogger<TransactionController> logger)
        {
            this._transactionService = transactionService;
            this._logger = logger;
        }


        /// <summary>
        /// Registers the customer.
        /// </summary>
        /// <param name="customerModel">The customer model.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("register-transaction")]
        public IActionResult RegisterTransaction(TransactionModel transaction)
        {
            try
            {
                var model = _transactionService.ExecuteTransaction(transaction);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("get-latest-transactionlist/{customerId}/{accountId}")]
        public IActionResult GetLatestTransactionList(string customerId, string accountId)
        {
            try
            {
                var result = _transactionService.GetLatestTransactionList(Convert.ToInt32(customerId), Convert.ToInt32(accountId));
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


        [HttpPost("get-transaction-paginated-list")]
        public async Task<ActionResult> GetTransactionPaginatedList([FromBody] TransactionFilter transactionFilter)
        {
            try
            {
                var list = await _transactionService.GetTransactionPaginatedList(transactionFilter);
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTransactionPaginatedList Error " + ex.Message);
                _logger.LogInformation(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }


    }
}
