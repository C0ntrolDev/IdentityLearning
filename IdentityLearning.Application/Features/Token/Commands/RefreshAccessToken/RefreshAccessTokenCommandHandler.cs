using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Application.DTOs.Identity.Token.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Token.Commands.RefreshAccessToken
{
    internal class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, Result<RefreshAndAccessTokenDto>>
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenBlacklist _tokenBlacklist;
        private readonly IMapper _mapper;

        public RefreshAccessTokenCommandHandler(ISessionRepository sessionRepository, IMapper mapper, ITokenRepository tokenRepository, ITokenBlacklist tokenBlacklist)
        {
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _tokenRepository = tokenRepository;
            _tokenBlacklist = tokenBlacklist;
        }

        public async Task<Result<RefreshAndAccessTokenDto>> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var validator = new RefreshAccessTokenValidator(_tokenRepository);
            var validateResult = await validator.ValidateAsync(request.RefreshAccessTokenDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult<RefreshAndAccessTokenDto>();
            }

            var accessTokenId = await _tokenRepository.GetTokenGuid(request.RefreshAccessTokenDto.AccessToken);
            if (accessTokenId == null)
            {
                return Result<RefreshAndAccessTokenDto>.NotSuccessfull("Can't get accessTokenId from accessToken", TotalErrorCode.BadRequest);
            }

            var refreshTokenId = await _tokenRepository.GetTokenGuid(request.RefreshAccessTokenDto.RefreshToken);
            if (refreshTokenId == null)
            {
                return Result<RefreshAndAccessTokenDto>.NotSuccessfull("Can't get refreshTokenId from refreshToken", TotalErrorCode.BadRequest);
            }

            var session = await _sessionRepository.GetSessionByTokens((Guid)refreshTokenId, (Guid)accessTokenId);
            if (session == null)
            {
                return Result<RefreshAndAccessTokenDto>.NotSuccessfull("Can't found session with specified accessToken and refreshToken", TotalErrorCode.NotFound);
            }

            if (session.AccessToken != null)
            {
                await _tokenBlacklist.AddAccessTokenInBlacklist(session.AccessToken);
            }

            var newRefreshAndAccessToken = await _sessionRepository.RefreshAccessToken(session);

            var newRefreshAndAccessTokenDto = _mapper.Map<RefreshAndAccessTokenDto>(newRefreshAndAccessToken);
            return Result<RefreshAndAccessTokenDto>.Successfull(newRefreshAndAccessTokenDto);
        }
    }
}
