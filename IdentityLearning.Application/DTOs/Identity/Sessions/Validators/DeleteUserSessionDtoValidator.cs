using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.Validators
{
    internal class DeleteUserSessionDtoValidator : AbstractValidator<DeleteUserSessionDto>
    {
        public DeleteUserSessionDtoValidator()
        {
            RuleFor(d => d.SessionId).NotNull();
            RuleFor(d => d.UserId).NotNull();
        }
    }
}
