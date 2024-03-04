using IdentityLearning.Application.Contracts.Identity;

namespace IdentityLearning.API.Extensions
{
    public static class TokenBlacklistCleanerExtension
    {
        public static IApplicationBuilder AddTokenBlacklistCleaning(this IApplicationBuilder builder)
        {
            var cleaner = builder.ApplicationServices.GetRequiredService<ITokenBlacklistCleaner>();
            cleaner.StartPeriodicClean();
            return builder;
        }
    }
}
