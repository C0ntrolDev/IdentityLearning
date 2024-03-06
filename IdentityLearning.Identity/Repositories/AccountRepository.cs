using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Exceptions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace IdentityLearning.Identity.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDeleteCodeGenerator _deleteCodeGenerator;

        public AccountRepository(UserManager<ApplicationUser> userManager, IDeleteCodeGenerator deleteCodeGenerator)
        {
            _userManager = userManager;
            _deleteCodeGenerator = deleteCodeGenerator;
        }

        public async Task CreateUser(ApplicationUser user, string password)
        {
            var createResult = await _userManager.CreateAsync(user, password);
            if (createResult.Succeeded == false)
            {
                throw new IdentityException(createResult);
            }
            var addToUserRoleResult = await _userManager.AddToRoleAsync(user, "User");
            if (addToUserRoleResult.Succeeded == false)
            {
                throw new IdentityException(addToUserRoleResult);
            }
        }

        public async Task<ApplicationUser?> GetUser(long userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            return user;
        }

        public async Task<ApplicationUser?> GetUserByCredentials(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.PasswordHash == null) return null;

            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return passwordVerificationResult == PasswordVerificationResult.Success ? user : null;
        }

        public async Task<bool> IsEmailFree(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user == null;
        }

        public async Task<string> GenerateEmailConfirmationCode(ApplicationUser user)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<Result<object>> ConfirmEmail(ApplicationUser user, string code)
        {   
            var confirmationResult = await _userManager.ConfirmEmailAsync(user, code);
            return Result<object>.FromIdentityResult(confirmationResult);
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded == false)
            {
                throw new IdentityException(updateResult);
            }
        }

        public async Task<Result<object>> UpdatePassword(ApplicationUser user, string newPassword)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var changePasswordResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            return Result<object>.FromIdentityResult(changePasswordResult);
        }

        public Task<string> GenerateDeleteUserCode(ApplicationUser user)
        {
            return Task.FromResult(_deleteCodeGenerator.GenerateDeleteCode(user));
        }

        public async Task<Result<object>> DeleteUser(ApplicationUser user, string code)
        {
            var validationResult = await _deleteCodeGenerator.ValidateDeleteCode(user, code);
            if (validationResult.IsSuccessfull == false)
            {
                return validationResult.WithTotalErrorCode(TotalErrorCode.Forbidden);
            }

            var deleteResult = await _userManager.DeleteAsync(user);
            return Result<object>.FromIdentityResult(deleteResult);
        }
    }
}
