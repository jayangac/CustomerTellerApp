using CutomerTeller.WebAPIApp.Core.Common;
using CutomerTeller.WebAPIApp.Model.Customer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Business.Interfaces
{
    public interface ICustomerService
    {
        ResponseModel RegisterCustomer(CustomerModel customerModel);
        Task<object> GetCustomerList(CustomerFilter customerFilter);
        bool UpdateCustomer(CustomerModel customerModel);
        bool DeleteCustomer(int customerId);

    }
}
