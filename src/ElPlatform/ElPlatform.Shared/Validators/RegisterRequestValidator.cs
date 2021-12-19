using ElPlatform.Shared.Models;
using FluentValidation;
namespace ElPlatform.Shared.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(p => p.Email)
                .NotEmpty().
                WithMessage("Email Is Requierd")
                .EmailAddress().
                WithMessage("Not a valid email address");
            RuleFor(p => p.FirstName)
               .NotEmpty().
               WithMessage("First Name Is Requierd");
            RuleFor(p => p.LastName)
               .NotEmpty().
               WithMessage("Last Name Is Requierd");
            RuleFor(p => p.Password)
               .NotEmpty().
               WithMessage("Password Is Requierd")
               .MinimumLength(6)
               .WithMessage("Password Can't be less than 6 char");
            RuleFor(p => p.ConfirmPassword)
              .Equal(p => p.Password)
              .WithMessage("Confirm Password dosen't match password");
        }
    }
}
