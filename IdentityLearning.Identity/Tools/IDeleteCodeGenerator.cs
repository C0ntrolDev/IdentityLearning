using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Identity.Tools
{
    public interface IDeleteCodeGenerator
    {
        public string GenerateDeleteCode(ApplicationUser user);

        public Task<Result<object>> ValidateDeleteCode(ApplicationUser user, string code);
    }
}
