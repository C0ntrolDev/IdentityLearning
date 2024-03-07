using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Application.Exceptions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityLearning.Identity.Repositories.UserRepository
{
    internal class AdminUserRepository : UserRepository, IAdminUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityDbContext _dbContext;

        public AdminUserRepository(UserManager<ApplicationUser> userManager, IdentityDbContext dbContext) : base(userManager, dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }


        public async Task<Result<object>> DeleteUser(ApplicationUser user)
        {
            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdmin)
            {
                return Result<object>.NotSuccessfull("Can't delete admin user", TotalErrorCode.Forbidden);
            }

            var deleteResult =  await _userManager.DeleteAsync(user);
            if (deleteResult.Succeeded == false)
            {
                throw new IdentityException(deleteResult);
            }

            return Result<object>.Successfull(null!);
        }

        public async Task DeleteAllUsers()
        {
            var userRole = await _dbContext.Set<ApplicationRole>().FirstOrDefaultAsync(r => r.NormalizedName == "USER");
            if (userRole == null) return;

            var usersIds = _dbContext.Set<IdentityUserRole<long>>()
                .Where(ur => ur.RoleId == userRole.Id)
                .Select(u => u.UserId);

            var users = _dbContext.Set<ApplicationUser>().Where(u => usersIds.Contains(u.Id));

            _dbContext.Set<ApplicationUser>().RemoveRange(users);
            await _dbContext.SaveChangesAsync();
        }
    }
}
