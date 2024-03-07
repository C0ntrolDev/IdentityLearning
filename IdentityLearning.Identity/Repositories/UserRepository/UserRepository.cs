using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Exceptions;
using IdentityLearning.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityLearning.Identity.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityDbContext _dbContext;

        public UserRepository(UserManager<ApplicationUser> userManager, IdentityDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
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

        public async Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            return await _dbContext.Users.Select(u => u).ToListAsync();
        }

        public async Task UpdateUser(ApplicationUser user)
        {
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult.Succeeded == false)
            {
                throw new IdentityException(updateResult);
            }
        }

        public async Task<ApplicationUser?> GetUserByCredentials(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || user.PasswordHash == null) return null;

            var passwordVerificationResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return passwordVerificationResult == PasswordVerificationResult.Success ? user : null;
        }
    }
}
