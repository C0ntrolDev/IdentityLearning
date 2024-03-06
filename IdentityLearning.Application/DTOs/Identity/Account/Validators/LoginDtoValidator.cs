using FluentValidation;
using FluentValidation.Results;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators
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
