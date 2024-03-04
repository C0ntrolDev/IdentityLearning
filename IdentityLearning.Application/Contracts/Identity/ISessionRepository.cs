using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;

namespace IdentityLearning.Application.Contracts.Identity
{
    public interface ISessionRepository
    {
        public Task DeleteSession(ApplicationUserSession session);

        public Task<ApplicationUserSession?> GetSessionById(long sessionId);

        public Task<ApplicationUserSession?> GetSessionByTokens(Guid refreshTokenId, Guid accessTokenId);

        public Task<RefreshAndAccessTokenResponse> RefreshAccessToken(ApplicationUserSession session);

        public Task<RefreshAndAccessTokenResponse> CreateUserSession(ApplicationUser user, ApplicationUserSessionInfo sessionInfo);

        public Task<IEnumerable<ApplicationUserSession>?> GetUserSessions(long userId); 
    }
}
