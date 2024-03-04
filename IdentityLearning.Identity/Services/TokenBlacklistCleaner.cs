using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Application.Options;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IdentityLearning.Identity.Services
{
    internal class TokenBlacklistCleaner : ITokenBlacklistCleaner, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TokenBlacklistCleaner> _logger;
        private readonly JwtTokenOptions _jwtTokenOptions;

        private CancellationTokenSource _cts = new CancellationTokenSource();
        private bool _isPeriodicCleanRunning;

        public TokenBlacklistCleaner(IOptions<JwtTokenOptions> options, IServiceScopeFactory serviceScopeFactory, ILogger<TokenBlacklistCleaner> logger)
        { 
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _jwtTokenOptions = options.Value;
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }

        public Task CallClean()
        {
            return CleanExpiredTokens();
        }

        public void StartPeriodicClean()
        {
            if (_isPeriodicCleanRunning)
            {
                return;
            }

            _isPeriodicCleanRunning = true;
            _ = RunPeriodicClean(_cts.Token);
        }

        public void StopPeriodicClean()
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();
            _isPeriodicCleanRunning = false;
        }

        private async Task RunPeriodicClean(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                await Task.Delay(delay: new TimeSpan(hours: 0, minutes: _jwtTokenOptions.AccessTokenExpiresInMinutes, seconds: 0), cancellationToken: ct);
                await CleanExpiredTokens();
            }
        }

        private async Task CleanExpiredTokens()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _logger.LogInformation("Expired blacklist tokens cleaning was called");

                var context = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                var accessTokensToDelete = context.BlacklistedAccessTokens.Where(t => t.Expiration < DateTime.Now);
                context.RemoveRange(accessTokensToDelete);
                await context.SaveChangesAsync();
            }
        }
    }
}
