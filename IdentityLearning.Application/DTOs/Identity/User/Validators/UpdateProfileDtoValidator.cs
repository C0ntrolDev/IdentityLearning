using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.DTOs.Identity.User.Validators.Common;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators
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
