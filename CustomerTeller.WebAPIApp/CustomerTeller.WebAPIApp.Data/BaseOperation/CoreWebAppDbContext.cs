using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CutomerTeller.WebAPIApp.Data.EntityModels;


namespace CutomerTeller.WebAPIApp.Data.BaseOperation
{
    public class CoreWebAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public CoreWebAppDbContext(DbContextOptions<CoreWebAppDbContext> options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

       
    }
}
