using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Exceptions;
using IdentityLearning.Application.Models;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Domain.Models;
using IdentityLearning.Identity.Models;
using IdentityLearning.Identity.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace IdentityLearning.Identity.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IdentityDbContext _dbContext;

        public SessionRepository(UserManager<ApplicationUser> userManager, ITokenGenerator tokenGenerator, IdentityDbContext dbContext)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _dbContext = dbContext;
        }

        public async Task DeleteSession(ApplicationUserSession session)
        {
            _dbContext.Sessions.Remove(session);
            await _dbContext.SaveChangesAsync();
        }

        public Task<ApplicationUserSession?> GetSessionById(long sessionId)
        {
            return _dbContext.Sessions
                .Include(s => s.AccessToken)
                .FirstOrDefaultAsync(s => s.Id == sessionId);
        }

        public Task<ApplicationUserSession?> GetSessionByTokens(Guid refreshTokenId, Guid accessTokenId)
        {
            return _dbContext.Sessions
                .Include(s => s.AccessToken)
                .FirstOrDefaultAsync(s =>
                    s.AccessToken != null &&
                    s.AccessToken.Id == accessTokenId &&
                    s.RefreshTokenId == refreshTokenId);
        }

        public async Task<RefreshAndAccessTokenResponse> RefreshAccessToken(ApplicationUserSession session)
        {
            var user = await _userManager.FindByIdAsync(session.ApplicationUserId.ToString());
            if (user == null)
            {
                throw new ArgumentException($"User with id: {session.ApplicationUserId}, selected from session with id: {session.Id} not found");
            }

            var credentials = await GenerateUserCredentials(user);

            var accessToken = credentials.accessToken.Info;

            session.AccessToken = accessToken;
            accessToken.SessionId = session.Id;

            session.RefreshTokenId = credentials.refreshToken.Info.Id;

            await _dbContext.TokenInfo.AddAsync(credentials.accessToken.Info); 
            _dbContext.Sessions.Update(session);

            await _dbContext.SaveChangesAsync();

            return new RefreshAndAccessTokenResponse(credentials.refreshToken.Jwt, credentials.accessToken.Jwt);
        }

        public async Task<RefreshAndAccessTokenResponse> CreateUserSession(ApplicationUser user, ApplicationUserSessionInfo sessionInfo)
        {
            var credentials = await GenerateUserCredentials(user);

            var session = new ApplicationUserSession()
            {
                ApplicationUserId = user.Id,
                AccessToken = credentials.accessToken.Info,
                RefreshTokenId = credentials.refreshToken.Info.Id,
                CreationTime = DateTime.Now.ToUniversalTime(),

                DeviceInfo = sessionInfo.DeviceInfo,
                DeviceName = sessionInfo.DeviceName,
                Location = sessionInfo.Location
            };

            await AddUserSession(session);

            return new RefreshAndAccessTokenResponse(credentials.refreshToken.Jwt, credentials.accessToken.Jwt);
        }

        private async Task<(JwtWithInfo refreshToken, JwtWithInfo accessToken)> GenerateUserCredentials(ApplicationUser user)           
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var refreshToken = await _tokenGenerator.GenerateRefreshToken();
            var accessToken = await _tokenGenerator.GenerateAccessToken(user, userClaims, userRoles);

            return (refreshToken, accessToken);
        }

        public async Task<IEnumerable<ApplicationUserSession>?> GetUserSessions(long userId)
        {
            return await _dbContext.Sessions.Where(s => s.ApplicationUserId == userId).ToListAsync();

        }

        private async Task AddUserSession(ApplicationUserSession session)
        {
            await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }
    }
}
