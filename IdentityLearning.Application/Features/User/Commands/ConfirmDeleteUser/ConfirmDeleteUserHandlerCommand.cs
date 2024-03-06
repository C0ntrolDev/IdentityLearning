using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.User.Commands.ConfirmDeleteUser
{
    public class ConfirmDeleteUserHandlerCommand : IRequestHandler<ConfirmDeleteUserCommand, Result<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenBlacklist _tokenBlacklist;

        public ConfirmDeleteUserHandlerCommand(IUserRepository userRepository, ITokenBlacklist tokenBlacklist, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _tokenBlacklist = tokenBlacklist;
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<object>> Handle(ConfirmDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.UserId);
            if (user == null)
            {
                return Result<object>.NotSuccessfull($"User with id: {request.UserId} not found", TotalErrorCode.NotFound);
            }

            var sessions = await _sessionRepository.GetUserSessions(user.Id);
            if (sessions != null)
            {
                foreach (var session in sessions)
                {
                    if (session.AccessToken != null)
                    {
                        await _tokenBlacklist.AddAccessTokenInBlacklist(session.AccessToken);
                    }

                    await _sessionRepository.DeleteSession(session);
                }
            }
            return await _userRepository.DeleteUser(user, request.Code);
        }
    }
}
