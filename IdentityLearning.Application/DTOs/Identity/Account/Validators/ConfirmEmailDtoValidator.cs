using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Account.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators
{
    internal class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(c => c.ConfirmationCode).NotNull().NotEmpty();
        }
    }
}
