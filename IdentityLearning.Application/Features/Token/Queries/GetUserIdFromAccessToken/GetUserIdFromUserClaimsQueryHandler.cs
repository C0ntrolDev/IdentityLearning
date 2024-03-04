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

namespace IdentityLearning.Application.Features.Token.Queries.GetUserIdFromAccessToken
{
    internal class GetUserIdFromUserClaimsQueryHandler : IRequestHandler<GetUserIdFromUserClaimsQuery, Result<long>>
    {
        private readonly ITokenRepository _tokenRepository;

        public GetUserIdFromUserClaimsQueryHandler(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task<Result<long>> Handle(GetUserIdFromUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetUserIdFromUserClaimsValidator();
            var validateResult = await validator.ValidateAsync(request.GetUserIdFromUserClaims, cancellationToken);
            if (validateResult.IsValid == false)
            {
                return validateResult.ToResult<long>();
            }

            var userId = await _tokenRepository.GetUserIdFromClaims(request.GetUserIdFromUserClaims.AccessTokenClaims!);
            return userId != null
                ? Result<long>.Successfull((long)userId!)
                : Result<long>.NotSuccessfull("AccessToken don't contain accessToken", TotalErrorCode.NotFound);
        }
    }
}
