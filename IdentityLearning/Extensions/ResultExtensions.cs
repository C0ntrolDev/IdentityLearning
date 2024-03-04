using IdentityLearning.API.Models.ActionResults;
using IdentityLearning.Domain.Exceptions;
using IdentityLearning.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToErrorActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccessfull)
            {
                throw new ResultSuccessfullException();
            }

            switch (result.TotalErrorCode)
            {
                case TotalErrorCode.BadRequest:
                    return new BadRequestObjectResult(result.Errors);
                case TotalErrorCode.Forbidden:
                    return new ForbidObjectResult(result.Errors);
                case TotalErrorCode.NotFound:
                    return new NotFoundObjectResult(result.Errors);
                case TotalErrorCode.Conflict:
                    return new ConflictObjectResult(result.Errors);
                case TotalErrorCode.Unauthorized:
                    return new UnauthorizedObjectResult(result.Errors);
                case TotalErrorCode.Gone:
                    return new GoneObjectResult(result.Errors);
                default:
                    return new InternalServerErrorObjectResult(result.Errors);
            }
        }
    }
}
