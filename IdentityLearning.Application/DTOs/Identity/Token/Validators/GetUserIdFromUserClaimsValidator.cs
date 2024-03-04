using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Token.Validators
{
    internal class GetUserIdFromUserClaimsValidator : AbstractValidator<GetUserIdFromUserClaimsDto>
    {
        public GetUserIdFromUserClaimsValidator()
        {
            RuleFor(g => g.AccessTokenClaims).NotNull().NotEmpty();
        }
    }
}
