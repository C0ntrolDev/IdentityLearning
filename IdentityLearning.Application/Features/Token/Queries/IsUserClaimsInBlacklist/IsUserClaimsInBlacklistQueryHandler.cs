using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Token.Validators;
using IdentityLearning.Application.Models.Extensions;
using IdentityLearning.Domain.Models;
using MediatR;

namespace IdentityLearning.Application.Features.Token.Queries.IsUserClaimsInBlacklist
{
    internal class IsUserClaimsInBlacklistQueryHandler : IRequestHandler<IsUserClaimsInBlacklistQuery, Result<bool>>
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenBlacklist _tokenBlacklist;

        public IsUserClaimsInBlacklistQueryHandler(ITokenRepository tokenRepository, ITokenBlacklist tokenBlacklist)
        {
            _tokenRepository = tokenRepository;
            _tokenBlacklist = tokenBlacklist;
        }

        public async Task<Result<bool>> Handle(IsUserClaimsInBlacklistQuery request, CancellationToken cancellationToken)
        {
            var validator = new IsUserClaimsInBlacklistValidator();
            var validateResult = await validator.ValidateAsync(request.IsUserClaimsInBlacklistDto, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult<bool>();
            }

            var tokenGuid = await _tokenRepository.GetTokenGuidFromClaims(request.IsUserClaimsInBlacklistDto.Claims);
            if (tokenGuid == null)
            {
                return Result<bool>.Successfull(false);
            }

            var isTokenInBlacklist = await _tokenBlacklist.IsAccessTokenInBlacklist((Guid)tokenGuid);
            return Result<bool>.Successfull(isTokenInBlacklist);
        }
    }
}
