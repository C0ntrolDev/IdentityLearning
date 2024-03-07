using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.DeleteAll
{
    internal class DeleteAllUsersCommandHandler : IRequestHandler<DeleteAllUsersCommand, Result<object>>
    {
        private readonly IAdminUserRepository _adminUserRepository;

        public DeleteAllUsersCommandHandler(IAdminUserRepository adminUserRepository)
        {
            _adminUserRepository = adminUserRepository;
        }

        public async Task<Result<object>> Handle(DeleteAllUsersCommand request, CancellationToken cancellationToken)
        {
            await _adminUserRepository.DeleteAllUsers();
            return Result<object>.Successfull(null!);
        }
    }

}
