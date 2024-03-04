using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Models.Extensions
{
    public static class ValidationResultExtensions
    {
        public static Result<object> ToResult(this ValidationResult validationResult)
        {
            if (validationResult.IsValid)
            {
                return Result<object>.Successfull(null!);
            }

            return Result<object>.NotSuccessfull(validationResult.Errors.Select(ve => ve.ToString()), TotalErrorCode.BadRequest);
        }

        public static Result<T> ToResult<T>(this ValidationResult validationResult, T result = default!)
        {
            if (validationResult.IsValid)
            {
                return Result<T>.Successfull(result);
            }

            return Result<T>.NotSuccessfull(validationResult.Errors.Select(ve => ve.ToString()), TotalErrorCode.BadRequest);
        }
    }
}
