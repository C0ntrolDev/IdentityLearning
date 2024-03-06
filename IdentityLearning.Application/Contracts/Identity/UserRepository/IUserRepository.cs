using IdentityLearning.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityLearning.Application.Contracts.Identity.UserRepository
{
    public interface IUserRepository
    {
        public Task CreateUser(ApplicationUser user, string password);
        public Task<ApplicationUser?> GetUser(long userId);
        public Task<ApplicationUser?> GetUserByCredentials(string email, string password);
        public Task UpdateUser(ApplicationUser user);
    }
}
