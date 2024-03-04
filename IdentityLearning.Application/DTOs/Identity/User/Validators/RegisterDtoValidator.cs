using FluentValidation;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.Validators.Common;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators
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
