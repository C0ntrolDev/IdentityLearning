using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.Features.Sessions.Queries.GetUserSessions;

namespace IdentityLearning.Application.DTOs.Identity.Sessions.Validators
{
    internal class GetUserSessionsDtoValidator : AbstractValidator<GetUserSessionsDto>
    {
        public GetUserSessionsDtoValidator()
        {
            RuleFor(g => g.UserId).NotNull();
        }
    }
}
