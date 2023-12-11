using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NetApiStarterApp.Data;
using NetApiStarterApp.Models.Account;

namespace NetApiStarterApp.Validators
{
    public class RegistrationValidator : AbstractValidator<AddAccountDto>
    {
        public RegistrationValidator(DataConnection dbContext)
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .Must(username => !dbContext.Accounts.Any(a => a.Username == username))
                .WithMessage("Username must be unique.")
                .Must(username => username.All(char.IsLetter))
                .WithMessage("Username must contain only letters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must(email => !dbContext.Accounts.Any(a => a.Email == email))
                .WithMessage("Email must be unique.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .Must(password =>
                    password.Any(char.IsLetter) &&
                    password.Any(char.IsDigit) &&
                    password.Any(ch => !char.IsLetterOrDigit(ch))
                )
                .WithMessage("Password must be at least 6 characters, contain letters, numbers, and at least one special character.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .WithMessage("Confirm Password must match Password.");
        }
    }

}
