using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Contracts.Identity.UserRepository
{
    public interface IAdminUserRepository : IUserRepository
    {
        public Task<Result<object>> DeleteUser(ApplicationUser user);

        public Task DeleteAllUsers();
    }
}
