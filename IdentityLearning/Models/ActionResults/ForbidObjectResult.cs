using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Models.ActionResults
{
    public class ForbidObjectResult : ObjectResult
    {
        public ForbidObjectResult(object? value) : base(value)
        {
            this.StatusCode = 403;
        }
    }

}
