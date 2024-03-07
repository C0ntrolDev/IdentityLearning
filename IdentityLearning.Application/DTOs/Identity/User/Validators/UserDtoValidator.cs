using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Common.Validators;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.User.Validators
{
    internal class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(u => u.Name).SetValidator(new NameValidator());
            RuleFor(u => u.Password).SetValidator(new PasswordValidator());
            RuleFor(u => u.Email).NotNull().NotEmpty().EmailAddress();
        }
    }
}
