using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Exceptions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace IdentityLearning.Identity.Repositories.UserRepository
{
    public class AccountUserRepository : UserRepository, IAccountUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDeleteCodeGenerator _deleteCodeGenerator;

        public AccountUserRepository(UserManager<ApplicationUser> userManager, IDeleteCodeGenerator deleteCodeGenerator) : base(userManager)
        {
            _userManager = userManager;
            _deleteCodeGenerator = deleteCodeGenerator;
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
