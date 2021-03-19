using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Extensions
{
    public static class UserManagerExtensions
    {
        public static Guid GetUserIdGuid(this UserManager<User> userManager, ClaimsPrincipal claimsPrincipal)
        {
            var userId = userManager.GetUserId(claimsPrincipal);
            if (Guid.TryParse(userId, out var result))
                return result;
            return Guid.Empty;
        }
    }
}