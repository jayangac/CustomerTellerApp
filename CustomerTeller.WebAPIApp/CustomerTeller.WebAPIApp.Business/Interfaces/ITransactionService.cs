using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Model.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Business.Interfaces
{
    public interface ITransactionService
    {
        ResponseModel ExecuteTransaction(TransactionModel customerModel);
        List<CustomerAccountTransactionVM> GetLatestTransactionList(int customerId, int accountId);
        Task<object> GetTransactionPaginatedList(TransactionFilter transactionFilter);
    }
}
