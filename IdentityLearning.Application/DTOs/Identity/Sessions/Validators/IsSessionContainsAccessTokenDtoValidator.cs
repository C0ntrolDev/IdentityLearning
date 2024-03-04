using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.Validators
{
    internal class IsSessionContainsAccessTokenDtoValidator : AbstractValidator<IsSessionContainsAccessTokenDto>
    {
        public IsSessionContainsAccessTokenDtoValidator()
        {
            RuleFor(i => i.AccessTokenClaims).NotNull().NotEmpty();
            RuleFor(i => i.SessionId).NotNull();
        }
    }
}
