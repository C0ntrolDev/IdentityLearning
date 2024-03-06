using IdentityLearning.Application.Contracts.Identity;
using IdentityLearning.Domain.Entities.User;
using IdentityLearning.Identity.Repositories;
using IdentityLearning.Identity.Services;
using IdentityLearning.Identity.Tools;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityLearning.Identity
{
    public static class IdentityRegistration
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(optionsBuilder =>
            {
                var connectionString = configuration.GetConnectionString("DbConnection");
                optionsBuilder.UseSqlite(connectionString);
            });

            services.AddIdentityCore<ApplicationUser>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequiredLength = 0;
                    o.Password.RequiredUniqueChars = 0;

                    o.User.RequireUniqueEmail = true;
                    o.SignIn.RequireConfirmedEmail = true;
                })
                .AddRoles<ApplicationRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<IdentityDbContext>();

            services.AddSingleton<IDeleteCodeGenerator, DeleteCodeGenerator>();
            services.AddSingleton<ITokenBlacklistCleaner, TokenBlacklistCleaner>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<ITokenBlacklist, TokenBlacklist>();

            return services;
        }
    }
}
