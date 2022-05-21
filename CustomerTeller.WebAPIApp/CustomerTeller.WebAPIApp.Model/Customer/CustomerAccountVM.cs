using System;

namespace CutomerTeller.WebAPIApp.Model.Customer
{
    public class CustomerAccountVM
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public Decimal Balance { get; set; }


    }
}
