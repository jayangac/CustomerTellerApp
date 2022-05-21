using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CutomerTeller.WebAPIApp.Model.Customer
{
    public class CustomerModel
    {
        //[JsonIgnore]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

    }
}
