using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators.Common
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
