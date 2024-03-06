using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Domain.Entities.User;

namespace IdentityLearning.Application.Contracts.Identity.UserRepository
{
    public interface IAdminUserRepository : IUserRepository
    {
        public Task DeleteUser(ApplicationUser user);

        public Task DeleteAllUsers();
    }
}
