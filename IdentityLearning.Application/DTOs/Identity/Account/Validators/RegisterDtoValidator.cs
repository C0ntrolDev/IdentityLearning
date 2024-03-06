using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.DTOs.Identity.Account.Validators.Common;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(r => r.Password)
                .SetValidator(new PasswordValidator());

            RuleFor(r => r.Name)
                .SetValidator(new NameValidator());
        }
    }
}
