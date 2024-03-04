using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace IdentityLearning.API.Filters.AccessTokenBlacklistAuthorize
{
    public class ExtendedAuthorizeAttribute : AuthorizeAttribute
    {
        public const string POLICY_PREFIX = "ExtendedAuthorize";

        public ExtendedAuthorizeAttribute()
        {
            Policy = POLICY_PREFIX;
        }
    }
}
