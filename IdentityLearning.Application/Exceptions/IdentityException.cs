using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Application.Exceptions
{
    public class IdentityException : ApplicationException
    {
        public IEnumerable<Error> Errors { get; } 

        public IdentityException(IdentityResult result)
        {
            Errors = result.Errors.Select(ie => new Error(ie.Code, ie.Description));
        }

        public override string ToString()
        {
            return $"IdentityException; Errors: {string.Join("", Errors.Select(e => $"{e.Code}: {e.Description}, "))}";
        }
    }
}
