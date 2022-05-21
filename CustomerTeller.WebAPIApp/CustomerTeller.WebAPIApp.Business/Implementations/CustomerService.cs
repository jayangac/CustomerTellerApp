using AutoMapper;
using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Data;
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
    /// <seealso cref="CutomerTeller.WebAPIApp.Business.Interfaces.ICustomerService" />
    public class CustomerService : ICustomerService
    {

        private readonly IMapper mapper;

        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public ResponseModel RegisterCustomer(CustomerModel customerModel)
        {

            var model = new ResponseModel();
            var _temp = IsCustomerRegistered(customerModel.Email);
            if (_temp)
            {
                model.Messsage = "Customer already registered.";
                model.IsSuccess = false;
            }
            else
            {
                var customer = mapper.Map<Customer>(customerModel);
                unitOfWork.GetRepository<Customer>().Insert(customer);
                model.IsSuccess = true;
                model.Messsage = "Customer registration completed.";
            }
            unitOfWork.SaveChanges();

            return model;
        }

        public bool IsCustomerRegistered(string Email)
        {
            var isExist = unitOfWork.GetRepository<Customer>().Get(x => x.Email.Contains(Email)).Any();

            if (isExist)
                return true;
            return false;
        }

        public async Task<object> GetCustomerList(CustomerFilter customerFilter)
        {
            var customerList= unitOfWork.GetRepository<Customer>().Get()
                .Select(v => new CustomerModel
                {
                    Id = (int)v.Id,
                    FirstName = v.FirstName,
                    LastName = v.LastName,
                    MiddleName = v.MiddleName,
                    DateOfBirth = v.DateOfBirth,
                    PhoneNumber = v.PhoneNumber,
                    Email = v.Email
                }).ToList();

            var custList = mapper.Map<List<CustomerModel>>(customerList);


            if (!string.IsNullOrEmpty(customerFilter.CustomerName))
            {
                custList = custList.Where(e => e.FirstName != null && e.FirstName.ToLower().Contains(customerFilter.CustomerName.ToLower())).ToList();
            }

            var total = custList.Count;

            if (Math.Ceiling((double)total / customerFilter.RecPerPage) >= customerFilter.PageNo)
            {
                var recList = custList.Skip(customerFilter.RecPerPage * (customerFilter.PageNo - 1)).Take(customerFilter.RecPerPage).ToList();
                var tranDtoList = mapper.Map<List<CustomerModel>>(recList);

                return new
                {
                    totRec = total,
                    totPgs = Math.Ceiling((double)total / customerFilter.RecPerPage),
                    pageNo = customerFilter.PageNo,
                    recPP = customerFilter.RecPerPage,
                    rows = tranDtoList
                };
            }

            return null;
        }

        public bool UpdateCustomer(CustomerModel customerModel)
        {
            var custId = Convert.ToInt32(customerModel.Id);
            var customer = unitOfWork.GetRepository<Customer>().Get(x => x.Id == custId).FirstOrDefault();
            customer.FirstName = customerModel.FirstName;
            customer.Email = customerModel.Email;
            unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteCustomer(int customerId)
        {
            var custId = Convert.ToInt32(customerId);
            var cus = unitOfWork.GetRepository<Customer>().Get(x => x.Id == custId).FirstOrDefault();
            unitOfWork.GetRepository<Customer>().Delete(custId);
            unitOfWork.SaveChanges();
            return true;
        }


    }
}
