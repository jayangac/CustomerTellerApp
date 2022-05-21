using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Data;
using CutomerTeller.WebAPIApp.Model;
using CutomerTeller.WebAPIApp.Model.Account;
using CutomerTeller.WebAPIApp.Model.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Business.Interfaces
{
    public interface IAccountService
    {
        ResponseModel RegisterAccount(AccountModel propertyModel);
        List<Account> GetAvailableAccountByCustomerId(int customerId);
        List<AccountModel> GetAccountList();
        bool UpdateAccount(AccountModel accountModel);
        bool DeleteAccount(int accountId);
        List<CustomerAccountVM> GetAccountsBalanceForCustomer(int customerId);
        CustomerAccountVM GetAccountBalance(int customerId, int accountId);
        bool IsAccountRegistered(string accountName);
        Task<object> GetAccountList(AccountFilter accountFilter);
    }
}
