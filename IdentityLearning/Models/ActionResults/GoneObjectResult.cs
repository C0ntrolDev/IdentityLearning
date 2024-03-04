using IdentityLearning.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace IdentityLearning.API.Models.ActionResults;

public class GoneObjectResult : ObjectResult
{
    public GoneObjectResult(object? value) : base(value)
    {
        this.StatusCode = 410;
    }
}