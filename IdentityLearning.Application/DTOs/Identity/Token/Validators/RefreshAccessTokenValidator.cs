using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.DTOs.Identity.Common;
using IdentityLearning.Application.DTOs.Identity.Token.DTOs;

namespace IdentityLearning.Application.DTOs.Identity.Token.Validators
{
    internal class RefreshAccessTokenValidator : AbstractValidator<RefreshAccessTokenDto>
    {
        public RefreshAccessTokenValidator(ITokenRepository tokenRepository)
        {
            RuleFor(r => r.AccessToken)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (accessToken, ct) => 
                    await tokenRepository.IsAccessTokenValidToRefresh(accessToken));

            RuleFor(r => r.RefreshToken)
                .NotNull()
                .NotEmpty()
                .MustAsync(async (refreshToken, ct) =>
                    await tokenRepository.IsRefreshTokenValid(refreshToken));
        }
    }
}
