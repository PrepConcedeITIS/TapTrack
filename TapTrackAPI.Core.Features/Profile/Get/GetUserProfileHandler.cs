using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Get
{
    [UsedImplicitly]
    public class GetUserProfileHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly UserManager<User> _userManager;
        public GetUserProfileHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await _userManager.GetUserAsync(query.ClaimsPrincipal);

            if (user == null)
                return new UserProfileDto(null, null, null);
            
            return new UserProfileDto(user.ProfileImageUrl, user.UserName, user.Email);
        }
    }
}