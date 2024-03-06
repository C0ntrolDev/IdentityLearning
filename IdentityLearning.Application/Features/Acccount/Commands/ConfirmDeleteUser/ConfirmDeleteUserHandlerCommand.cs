using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Contracts.Identity.UserRepository.IdentityLearning.Application.Contracts.Identity.UserRepository;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Acccount.Commands.ConfirmDeleteUser
{
    public class ConfirmDeleteUserHandlerCommand : IRequestHandler<ConfirmDeleteUserCommand, Result<object>>
    {
        private readonly IAccountUserRepository _accountUserUserRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenBlacklist _tokenBlacklist;

        public ConfirmDeleteUserHandlerCommand(IAccountUserRepository accountUserUserRepository, ITokenBlacklist tokenBlacklist, ISessionRepository sessionRepository)
        {
            _accountUserUserRepository = accountUserUserRepository;
            _tokenBlacklist = tokenBlacklist;
            _sessionRepository = sessionRepository;
        }

        public async Task<Result<object>> Handle(ConfirmDeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _accountUserUserRepository.GetUser(request.UserId);
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
            return await _accountUserUserRepository.DeleteUser(user, request.Code);
        }
    }
}
