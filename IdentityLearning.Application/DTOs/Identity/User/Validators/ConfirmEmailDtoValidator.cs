using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators
{
    internal class ConfirmEmailDtoValidator : AbstractValidator<ConfirmEmailDto>
    {
        public ConfirmEmailDtoValidator()
        {
            RuleFor(c => c.ConfirmationCode).NotNull().NotEmpty();
        }
    }
}
