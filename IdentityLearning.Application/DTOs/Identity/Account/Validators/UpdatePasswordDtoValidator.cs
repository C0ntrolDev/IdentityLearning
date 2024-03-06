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
    public class UpdatePasswordDtoValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordDtoValidator()
        {
            RuleFor(u => u.NewPassword)
                .SetValidator(new PasswordValidator());
        }
    }
}
