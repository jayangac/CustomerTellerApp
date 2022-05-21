using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CutomerTeller.WebAPIApp.Model
{
    public  class AccountModel
    {
        //[JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string CustomerName { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public DateTime? CreatedDateTime { get; set; }

    }
}
