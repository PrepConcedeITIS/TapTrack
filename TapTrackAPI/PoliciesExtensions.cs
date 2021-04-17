using Microsoft.AspNetCore.Authorization;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI
{
    public static class PoliciesExtensions
    {
        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Policies.Admin).Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(Policies.User).Build();
        }
    }
}