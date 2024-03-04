using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators.Common
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
