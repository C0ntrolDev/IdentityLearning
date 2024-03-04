using IdentityLearning.API.Extensions;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;
using IdentityLearning.Application.Features.Token.Queries.IsUserClaimsInBlacklist;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize
{
    public class ExtendedAuthorizeHandler : AuthorizationHandler<ExtendedAuthorizeRequirement>
    {
        private readonly IMediator _mediator;

        public ExtendedAuthorizeHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ExtendedAuthorizeRequirement requirement)
        {
            var claims = context.User.Claims;
            var isUserClaimsInBlacklistDto = new IsUserClaimsInBlacklistDto()
            {
                Claims = claims
            };

            var isUserClaimsInBlacklistResult = await _mediator.Send(new IsUserClaimsInBlacklistQuery(isUserClaimsInBlacklistDto));
            if (isUserClaimsInBlacklistResult.IsSuccessfull == false)
            {
                context.Fail(new AuthorizationFailureReason(this, isUserClaimsInBlacklistResult.ToString()));
                return;
            }

            if (isUserClaimsInBlacklistResult.Body)
            {
                context.Fail(new AuthorizationFailureReason(this, "User access token in blacklist, please refresh it!"));
                return;
            }

            context.Succeed(requirement);
        }
    }
}
