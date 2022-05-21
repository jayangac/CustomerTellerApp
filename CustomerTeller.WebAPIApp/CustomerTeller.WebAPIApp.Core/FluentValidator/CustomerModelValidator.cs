using CutomerTeller.WebAPIApp.Model.Customer;
using FluentValidation;

namespace CutomerTeller.WebAPIApp.Core.FluentValidator
{
    public  class CustomerModelValidator : AbstractValidator<CustomerModel>
    {
        public CustomerModelValidator()
        {
            RuleFor(validator => validator.FirstName).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
            RuleFor(validator => validator.Email).NotNull().NotEmpty().WithMessage("Email cannot be empty.");
            RuleFor(validator => validator.Email)
                  .NotEmpty()
                  .WithMessage("Email required")
                  .Length(0, 50)
                  .WithMessage("Maximum length allowed is 50 characters")
                  .Matches(
                      @"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$")
                  .WithMessage("Please enter a valid e-mail address");
        }

    }
}
