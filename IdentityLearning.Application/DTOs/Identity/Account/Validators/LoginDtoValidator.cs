using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(l => l.Password)
                .NotNull()
                .NotEmpty();
        }
    }
}
