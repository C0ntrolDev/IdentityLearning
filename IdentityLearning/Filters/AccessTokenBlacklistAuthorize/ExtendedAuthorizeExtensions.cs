using Microsoft.AspNetCore.Authorization;

namespace IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize
{
    public static class ExtendedAuthorizeExtensions
    {
        public static void AddExtendedAuthorizeHandler(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthorizationHandler, ExtendedAuthorizeHandler>();
        }

        public static void AddExtendedAuthorizeRequirement(this AuthorizationOptions options)
        {
            options.AddPolicy(ExtendedAuthorizeAttribute.POLICY_PREFIX, (policyBuilder) =>
            {
                policyBuilder.Combine(options.DefaultPolicy)
                    .AddRequirements(new ExtendedAuthorizeRequirement())
                    .Build();
            });
        }
    }
}
