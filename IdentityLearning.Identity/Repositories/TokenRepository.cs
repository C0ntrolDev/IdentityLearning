using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Models;
using IdentityLearning.Application.Options;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Tools;

namespace IdentityLearning.Identity.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly ITokenGenerator _tokenGenerator;

        public TokenRepository(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        public Task<Guid?> GetTokenGuid(string token)
        {
            var claims = _tokenGenerator.GetClaimsFromToken(token);
            if (claims == null)
            {
                throw new ArgumentException($"Can't get claims from token: {token}");
            }

            return Task.FromResult(_tokenGenerator.GetGuidFromClaims(claims));
        }

        public Task<Guid?> GetTokenGuidFromClaims(IEnumerable<Claim> claims)
        {
            return Task.FromResult(_tokenGenerator.GetGuidFromClaims(claims));
        }

        public Task<long?> GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            return Task.FromResult(_tokenGenerator.GetUserIdFromClaims(claims));
        }

        public Task<bool> IsAccessTokenValidToRefresh(string accessToken)
        {
            return _tokenGenerator.IsAccessTokenValidToRefresh(accessToken);
        }

        public Task<bool> IsRefreshTokenValid(string refreshToken)
        {
            return _tokenGenerator.IsRefreshTokenValid(refreshToken);
        }
    }
}
