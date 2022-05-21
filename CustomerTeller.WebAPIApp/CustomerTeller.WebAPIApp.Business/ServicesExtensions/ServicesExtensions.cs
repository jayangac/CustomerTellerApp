using CutomerTeller.WebAPIApp.Business.Implementations;
using CutomerTeller.WebAPIApp.Business.Interfaces;
using CutomerTeller.WebAPIApp.Repository.BaseRepository;
using CutomerTeller.WebAPIApp.Repository.BaseRepository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CutomerTeller.WebAPIApp.Business.ServicesExtensions
{
    public static class ServicesExtensions
    {

        /// <summary>
        /// Adds my library services.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMyLibraryServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAccountService, AccountService>();            
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
