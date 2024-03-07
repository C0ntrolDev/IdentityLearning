using FluentValidation;

namespace IdentityLearning.Application.DTOs.Identity.Common.Validators
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
