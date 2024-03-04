using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Sessions.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Sessions.Queries.IsSessionContainsAccessToken
{
    public class IsSessionContainsAccessTokenQueryHandler : IRequestHandler<IsSessionContainsAccessTokenQuery, Result<bool>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenRepository _tokenRepository;

        public IsSessionContainsAccessTokenQueryHandler(ISessionRepository sessionRepository, ITokenRepository tokenRepository)
        {
            _sessionRepository = sessionRepository;
            _tokenRepository = tokenRepository;
        }

        public async Task<Result<bool>> Handle(IsSessionContainsAccessTokenQuery request, CancellationToken cancellationToken)
        {
            var validator = new IsSessionContainsAccessTokenDtoValidator();
            var validateResult = await validator.ValidateAsync(request.IsSessionContainsAccessTokenDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult<bool>();
            }

            var session = await _sessionRepository.GetSessionById((long)request.IsSessionContainsAccessTokenDto.SessionId!);
            if (session == null)
            {
                return Result<bool>.NotSuccessfull($"Session with id: {request.IsSessionContainsAccessTokenDto.SessionId} not found", TotalErrorCode.NotFound);
            }

            var accessTokenId = await _tokenRepository.GetTokenGuidFromClaims(request.IsSessionContainsAccessTokenDto.AccessTokenClaims!);
            if (accessTokenId == null)
            {
                return Result<bool>.NotSuccessfull($"AccessToken don't contain guid: {request.IsSessionContainsAccessTokenDto.AccessTokenClaims}", TotalErrorCode.NotFound);
            }

            return Result<bool>.Successfull(session.AccessToken?.Id == accessTokenId);
        }
    }
}
