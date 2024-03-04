using IdentityLearning.Domain.Entities.User;

namespace IdentityLearning.Application.Contracts.Identity
{
    public interface ITokenBlacklist
    {
        public Task<bool> IsAccessTokenInBlacklist(Guid accessTokenId);

        public Task AddAccessTokenInBlacklist(TokenInfo accessToken);
    }
}
