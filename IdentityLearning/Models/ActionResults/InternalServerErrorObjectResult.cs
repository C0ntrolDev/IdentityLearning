using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Models.ActionResults
{
    public class InternalServerErrorObjectResult : ObjectResult
    {    
        public InternalServerErrorObjectResult(object? value) : base(value)
        {
            this.StatusCode = 500;
        }
    }
}
