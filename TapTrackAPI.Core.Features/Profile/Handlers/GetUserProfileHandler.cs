using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class GetUserProfileHandler : ProfileHandlerBase<GetUserProfileQuery, UserProfileDto>
    {

        public GetUserProfileHandler(UserManager<User> userManager) : base(userManager)
        {
        }

        public override async Task<UserProfileDto> Handle(GetUserProfileQuery query)
        {
            var user = await UserManager.GetUserAsync(query.ClaimsPrincipal);

            if (user == null)
                return new UserProfileDto(null, null, null);
            
            //TODO: поменять ProfileImageLink на нормальный
            return new UserProfileDto("null", user.UserName, user.Email);
        }
    }
}