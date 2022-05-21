using AutoMapper;
using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Data;
using CutomerTeller.WebAPIApp.Data.EntityModels;
using CutomerTeller.WebAPIApp.Model.Transaction;
using CutomerTeller.WebAPIApp.Repository.BaseRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Business.Implementations
{
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// The mapper.
        /// </summary>
        private readonly IMapper _mapper;


        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;


        public TransactionService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public ResponseModel ExecuteTransaction(TransactionModel transactionModel)
        {
            var model = new ResponseModel();
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var transaction = _mapper.Map<Transaction>(transactionModel);
                    const string includedEntities = "Customer";
                    var account = _unitOfWork.GetRepository<Account>().Get(includeProperties: includedEntities).
                        Where(x => x.Id == transactionModel.AccountId).FirstOrDefault();

                    _unitOfWork.GetRepository<Transaction>().Insert(transaction);

                    if (transactionModel.TransactionType == TransactionType.Deposit)
                        account.Balance = account.Balance + transactionModel.Amount;
                    else if (transactionModel.TransactionType == TransactionType.Withdrawal)
                        account.Balance = account.Balance - transactionModel.Amount;

                    model.IsSuccess = true;
                    model.Messsage = "Transaction completed.";

                    _unitOfWork.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return model;
        }


        public List<CustomerAccountTransactionVM> GetLatestTransactionList(int customerId, int accountId)
        {
            return _unitOfWork.GetRepository<Transaction>().Get().Where(x => x.Account.CustomerId == customerId && x.AccountId == accountId)
                .Select(v => new CustomerAccountTransactionVM
                {
                    AccountId = v.AccountId,
                    TransactionDateTime = v.TransactionDateTime,
                    Amount = v.Amount,
                    CustomerId = v.Account.CustomerId,
                }).OrderByDescending(x => x.Id).Take(10).ToList();
        }

        public async Task<object> GetTransactionPaginatedList(TransactionFilter transactionFilter)
        {
            var transactiontList = _unitOfWork.GetRepository<Transaction>().Get()
                                        .Select(v => new TransactionModel
                                        {
                                            TransactionDateTime = v.TransactionDateTime,
                                            Amount = v.Amount,
                                            TransactionTypeId = (int)v.TransactionType,
                                            AccountId = v.AccountId,
                                            AccountName = v.Account.Name,
                                        }).ToList();

            var transList = _mapper.Map<List<TransactionModel>>(transactiontList);


            if (!string.IsNullOrEmpty(transactionFilter.AccountName))
            {
                transList = transList.Where(e => e.AccountName != null && e.AccountName.ToLower().Contains(transactionFilter.AccountName.ToLower())).ToList();
            }

            var total = transList.Count;

            if (Math.Ceiling((double)total / transactionFilter.RecPerPage) >= transactionFilter.PageNo)
            {
                var recList = transList.Skip(transactionFilter.RecPerPage * (transactionFilter.PageNo - 1)).Take(transactionFilter.RecPerPage).ToList();
                var tranDtoList = _mapper.Map<List<TransactionModel>>(recList);

                return new
                {
                    totRec = total,
                    totPgs = Math.Ceiling((double)total / transactionFilter.RecPerPage),
                    pageNo = transactionFilter.PageNo,
                    recPP = transactionFilter.RecPerPage,
                    rows = tranDtoList
                };
            }

            return null;
        }
    }
}
