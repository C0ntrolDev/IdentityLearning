using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.DTOs.Identity.Account.Validators.Common;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators
{
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(u => u.NewPassword)
                .SetValidator(new PasswordValidator());
        }
    }
}
