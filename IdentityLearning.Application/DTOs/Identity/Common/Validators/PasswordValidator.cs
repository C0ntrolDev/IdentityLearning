using FluentValidation;

namespace IdentityLearning.Application.DTOs.Identity.Common.Validators
{
    internal class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator()
        {
            RuleFor(password => password).NotNull()
                .MinimumLength(8)
                .MaximumLength(16);
        }
    }
}
