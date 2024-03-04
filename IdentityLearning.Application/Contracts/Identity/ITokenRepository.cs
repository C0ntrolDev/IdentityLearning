using System.Security.Claims;
using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Contracts.Identity
{
    public interface ITokenRepository
    {
        public Task<Guid?> GetTokenGuid(string token);

        public Task<Guid?> GetTokenGuidFromClaims(IEnumerable<Claim> claims);
            
        public Task<long?> GetUserIdFromClaims(IEnumerable<Claim> claims);

        public Task<bool> IsAccessTokenValidToRefresh(string accessToken);

        public Task<bool> IsRefreshTokenValid(string refreshToken);
    }
}   
    