using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Sessions.DTOs;
using IdentityLearning.Application.DTOs.Identity.Sessions.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Commands.DeleteUserSession
{
    public class DeleteUserSessionCommandHandler : IRequestHandler<DeleteUserSessionCommand, Result<object>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenBlacklist _tokenBlacklist;

        public DeleteUserSessionCommandHandler(ISessionRepository sessionRepository, ITokenBlacklist tokenBlacklist)
        {
            _sessionRepository = sessionRepository;
            _tokenBlacklist = tokenBlacklist;
        }

        public async Task<Result<object>> Handle(DeleteUserSessionCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserSessionDtoValidator();
            var validateResult = await validator.ValidateAsync(request.DeleteUserSessionDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult();
            }

            var session = await _sessionRepository.GetSessionById((long)request.DeleteUserSessionDto.SessionId!);
            if (session == null)
            {
                return Result<object>.NotSuccessfull($"Session with id: {request.DeleteUserSessionDto.SessionId} not found", TotalErrorCode.NotFound);
            }

            if (session.ApplicationUserId != request.DeleteUserSessionDto.UserId)
            {
                return Result<object>.NotSuccessfull($"Session with id: {request.DeleteUserSessionDto.SessionId} not found " +
                                                     $"on user with id: {request.DeleteUserSessionDto.UserId}", TotalErrorCode.NotFound);
            }

            await _sessionRepository.DeleteSession(session);

            if (session.AccessToken != null)
            {
                await _tokenBlacklist.AddAccessTokenInBlacklist(session.AccessToken);
            }

            return Result<object>.Successfull(null!);
        }
    }
}
