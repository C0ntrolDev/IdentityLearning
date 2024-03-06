using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Domain.Entities.User;
using Microsoft.AspNetCore.Identity;

namespace IdentityLearning.Identity.Repositories.UserRepository
{
    internal class AdminUserRepository : UserRepository, IAdminUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminUserRepository(UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _userManager = userManager;
        }


    }
}
