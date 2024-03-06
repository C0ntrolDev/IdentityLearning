using FluentValidation.Results;
using IdentityLearning.Application.DTOs.Identity.User.DTOs;
using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Contracts.Identity
{
    public interface IAccountRepository
    {
        public Task CreateUser(ApplicationUser user, string password);
        public Task<ApplicationUser?> GetUser(long userId);
        public Task UpdateUser(ApplicationUser user);

        public Task<ApplicationUser?> GetUserByCredentials(string email, string password);

        public Task<bool> IsEmailFree(string email);

        public Task<string> GenerateEmailConfirmationCode(ApplicationUser user);

        public Task<Result<object>> ConfirmEmail(ApplicationUser user, string code);

        public Task<Result<object>> UpdatePassword(ApplicationUser user, string newPassword);

        public Task<string> GenerateDeleteUserCode(ApplicationUser user);

        public Task<Result<object>> DeleteUser(ApplicationUser user, string code);
    }
}
