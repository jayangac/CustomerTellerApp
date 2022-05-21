using AutoMapper;
using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Data;
using CutomerTeller.WebAPIApp.Model;
using CutomerTeller.WebAPIApp.Model.Account;
using CutomerTeller.WebAPIApp.Model.Customer;
using CutomerTeller.WebAPIApp.Repository.BaseRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Business.Implementations
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="CutomerTeller.WebAPIApp.Business.Interfaces.IAccountService" />
    public class AccountService : IAccountService
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper mapper;

        public AccountService(
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this._unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Registers the account.
        /// </summary>
        /// <param name="accountModel">The account model.</param>
        /// <returns></returns>
        public ResponseModel RegisterAccount(AccountModel accountModel)
        {
            var model = new ResponseModel();
            var _temp = IsAccountRegistered(accountModel.Name);
            if (_temp)
            {
                model.Messsage = "Account already registered.";
                model.IsSuccess = false;
            }
            else
            {
                var account = mapper.Map<Account>(accountModel);
                _unitOfWork.GetRepository<Account>().Insert(account);
                model.IsSuccess = true;
                model.Messsage = "Account registration completed.";
            }
            _unitOfWork.SaveChanges();
            return model;
        }

        /// <summary>
        /// Determines whether Account is registered.
        /// </summary>
        /// <param name="accountName">The identifier accountName.</param>
        /// <returns>
        /// </returns>
        public bool IsAccountRegistered(string accountName)
        {
            var isExist = _unitOfWork.GetRepository<Account>().Get()
           .Where(b => b.Name.Contains(accountName))
           .Any();

            if (isExist)
                return true;
            return false;
        }

        /// <summary>
        /// 1. Input Customer ID; able to retrieve account balances for all accounts for that customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<CustomerAccountVM> GetAccountsBalanceForCustomer(int customerId)
        {
            return _unitOfWork.GetRepository<Account>().Get(includeProperties: "Customer")
                .Where(x => x.CustomerId == customerId)
                .Select(v => new CustomerAccountVM
                {
                    AccountId = v.Id,
                    CustomerId = v.CustomerId,
                    CustomerName = v.Customer.FirstName + " " + v.Customer.MiddleName + " " + v.Customer.LastName,
                    Name = v.Name,
                    Balance = v.Balance,
                }).ToList();
        }

        /// <summary>
        /// Gets the available accounts custom domains.
        /// </summary>
        /// <param name="customerId">The customerId identifier.</param>
        /// <returns>Domain list.</returns>
        public List<Account> GetAvailableAccountByCustomerId(int customerId)
        {
            var list = _unitOfWork.GetRepository<Account>().Get(x => x.CustomerId == customerId).ToList();
            return list;
        }

        /// <summary>
        /// Gets the account list.
        /// </summary>
        /// <returns></returns>
        public List<AccountModel> GetAccountList()
        {
            return _unitOfWork.GetRepository<Account>().Get().Select(v => new AccountModel
            {
                Id = v.Id,
                Name = v.Name,
                Balance = v.Balance,
                CustomerId = v.CustomerId
            }).ToList(); ;
        }

        /// <summary>
        /// 2. Input customer ID, Account able to retrieve account balances for that account
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public CustomerAccountVM GetAccountBalance(int customerId, int accountId)
        {
            return _unitOfWork.GetRepository<Account>().Get(includeProperties: "Customer")
                .Where(x => x.CustomerId == customerId && x.Id == accountId)
                .Select(v => new CustomerAccountVM
                {
                    AccountId = v.Id,
                    CustomerId = v.CustomerId,
                    Name = v.Name,
                    Balance = v.Balance,
                }).FirstOrDefault();
        }

        public bool UpdateAccount(AccountModel accountModel)
        {
            var account = _unitOfWork.GetRepository<Account>().Get(x => x.Id == accountModel.Id).FirstOrDefault();
            account.Name = accountModel.Name;
            _unitOfWork.SaveChanges();
            return true;
        }
        public bool DeleteAccount(int accountId)
        {
            var account = _unitOfWork.GetRepository<Account>().Get(x => x.Id == accountId).FirstOrDefault();
            _unitOfWork.GetRepository<Account>().Delete(accountId);
            _unitOfWork.SaveChanges();
            return true;
        }

        public async Task<object> GetAccountList(AccountFilter accountFilter)
        {
            var accountList = _unitOfWork.GetRepository<Account>().Get(includeProperties: "Customer")
                .Select(v => new AccountModel
                {
                    Id = v.Id,
                    CustomerId = v.CustomerId,
                    Name = v.Name,
                    Balance = v.Balance,
                    CustomerName = v.Customer.FirstName + " " + v.Customer.MiddleName + " " + v.Customer.LastName,
                }).ToList();

            var accList = mapper.Map<List<AccountModel>>(accountList);


            if (!string.IsNullOrEmpty(accountFilter.CustomerName))
            {
                accList = accList.Where(e => e.CustomerName != null && e.CustomerName.ToLower().Contains(accountFilter.CustomerName.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(accountFilter.AccountName))
            {
                accList = accList.Where(e => e.Name != null && e.Name.ToLower().Contains(accountFilter.AccountName.ToLower())).ToList();
            }

            var total = accList.Count;

            if (Math.Ceiling((double)total / accountFilter.RecPerPage) >= accountFilter.PageNo)
            {
                var recList = accList.Skip(accountFilter.RecPerPage * (accountFilter.PageNo - 1)).Take(accountFilter.RecPerPage).ToList();
                var accountDtoList = mapper.Map<List<AccountModel>>(recList);

                return new
                {
                    totRec = total,
                    totPgs = Math.Ceiling((double)total / accountFilter.RecPerPage),
                    pageNo = accountFilter.PageNo,
                    recPP = accountFilter.RecPerPage,
                    rows = accountDtoList
                };
            }

            return null;
        }
    }
}
