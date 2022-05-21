using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CutomerTeller.WebAPIApp.Model.Transaction
{
  public class TransactionModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime? TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        public int TransactionTypeId { get; set; }
        public TransactionType TransactionType { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }

    }
}
