using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;
using IdentityLearning.Application.DTOs.Identity.Account.Validators.Common;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators
{
    internal class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileDtoValidator()
        {
            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Name)
                .SetValidator(new NameValidator());
        }
    }
}
