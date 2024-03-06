using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Infrastructure;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.DeleteUser
{
    internal class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;

        public DeleteUserCommandHandler(IUserRepository userRepository, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _emailSender = emailSender;
        }

        public async Task<Result<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var code = await _userRepository.GenerateDeleteUserCode(user);
            _emailSender.SendDeleteAccountConfirmationMessage(user.Email!, code, request.UserId);

            return Result<object>.Successfull(null!);
        }
    }
}
