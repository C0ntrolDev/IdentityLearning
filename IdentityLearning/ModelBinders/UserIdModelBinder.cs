using System.Net;
using IdentityLearning.API.Extensions;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;
using IdentityLearning.Application.Features.Token.Queries.GetUserIdFromAccessToken;
using IdentityLearning.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IdentityLearning.API.ModelBinders
{
    public class UserIdModelBinder : IModelBinder
    {
        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.ActionContext.HttpContext.User.Identity is { IsAuthenticated: true })
            {
                var mediator = bindingContext.ActionContext.HttpContext.RequestServices.GetRequiredService<IMediator>();
                var userClaims = bindingContext.ActionContext.HttpContext.User.Claims;

                var getUserIdFromUserClaimsDto = new GetUserIdFromUserClaimsDto()
                {
                    AccessTokenClaims = userClaims
                };

                var getUserIdResult = await mediator.Send(new GetUserIdFromUserClaimsQuery(getUserIdFromUserClaimsDto));
                if (getUserIdResult.IsSuccessfull == false)
                {
                    foreach (var error in getUserIdResult.Errors)
                    {
                        bindingContext.ModelState.AddModelError("Authorization", error.Description);
                    }
                    return;
                }

                bindingContext.Result = ModelBindingResult.Success(getUserIdResult.Body);
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
    }
}
