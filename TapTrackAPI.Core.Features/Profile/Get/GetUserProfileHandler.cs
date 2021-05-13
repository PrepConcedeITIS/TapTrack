using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetUserProfileHandler : ProfileHandlerBase<GetUserProfileQuery, UserProfileDto>
    {

        public GetUserProfileHandler(UserManager<User> userManager) : base(userManager)
        {
        }

        public override async Task<UserProfileDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            if (user == null)
                return new UserProfileDto(null, null, null);
            
            return new UserProfileDto(user.ProfileImageUrl, user.UserName, user.Email);
        }
    }
}