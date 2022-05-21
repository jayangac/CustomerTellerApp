using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Model.Transaction
{
    public class CustomerAccountTransactionVM
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public int TransactionType { get; set; }
    }
}
