using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Identity.Models;

namespace IdentityLearning.Identity.Repositories
{
    public class TokenBlacklist : ITokenBlacklist
    {
        private readonly IdentityDbContext _context;

        public TokenBlacklist(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAccessTokenInBlacklist(Guid accessTokenId)
        {
            var foundBlacklistedAccessToken = await _context.BlacklistedAccessTokens.FindAsync(accessTokenId);
            return foundBlacklistedAccessToken != null;
        }

        public async Task AddAccessTokenInBlacklist(TokenInfo accessToken)
        {
            await _context.BlacklistedAccessTokens.AddAsync(new BlacklistedAccessToken()
            {
                Id = accessToken.Id, 
                Expiration = accessToken.Expiration
            });
            await _context.SaveChangesAsync();
        }
    }
}
