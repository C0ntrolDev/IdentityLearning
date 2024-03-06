using FluentValidation;

namespace IdentityLearning.Application.DTOs.Identity.Account.Validators.Common
{
    internal class NameValidator : AbstractValidator<string>
    {
        public NameValidator()
        {
            RuleFor(name => name)
                .NotNull()
                .Length(min: 4, max: 20);
        }
    }
}
