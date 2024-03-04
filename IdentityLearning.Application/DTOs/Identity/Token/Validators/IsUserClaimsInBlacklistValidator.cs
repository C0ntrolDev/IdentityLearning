using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Token.Validators
{
    internal class IsUserClaimsInBlacklistValidator : AbstractValidator<IsUserClaimsInBlacklistDto>
    {
        public IsUserClaimsInBlacklistValidator()
        {
            RuleFor(i => i.Claims).NotNull();
        }
    }
}
