using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Validators;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Models;

namespace IdentityLearning.Identity.Tools
{
    public interface ITokenGenerator
    {
        public Task<JwtWithInfo> GenerateAccessToken(ApplicationUser user, IEnumerable<Claim> userClaims, IEnumerable<string> roles);
        public Task<JwtWithInfo> GenerateRefreshToken();
        public Guid? GetGuidFromClaims(IEnumerable<Claim> claims);
        public long? GetUserIdFromClaims(IEnumerable<Claim> claims);
        IEnumerable<Claim>? GetClaimsFromToken(string token);
        public Task<bool> IsAccessTokenValidToRefresh(string accessToken);
        public Task<bool> IsRefreshTokenValid(string refreshToken);
    }
}
        