using CutomerTeller.WebAPIApp.Model.Transaction;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CutomerTeller.WebAPIApp.Data.EntityModels
{
    public  class Transaction : BaseEntity
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public decimal Amount { get; set; }
        //public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }


    }
}
