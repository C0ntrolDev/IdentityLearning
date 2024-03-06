using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Contracts.Identity.UserRepository
{
    namespace IdentityLearning.Application.Contracts.Identity.UserRepository
    {
        public interface IAccountUserRepository : IUserRepository
        {
            public Task<bool> IsEmailFree(string email);

            public Task<string> GenerateEmailConfirmationCode(ApplicationUser user);

            public Task<Result<object>> ConfirmEmail(ApplicationUser user, string code);

            public Task<Result<object>> UpdatePassword(ApplicationUser user, string newPassword);

            public Task<string> GenerateDeleteUserCode(ApplicationUser user);

            public Task<Result<object>> DeleteUser(ApplicationUser user, string code);
        }
    }
}
