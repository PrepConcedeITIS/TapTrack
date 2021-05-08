using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class ChangeUserNameHandler : ProfileHandlerWithDbContextBase<ChangeUserNameCommand, UserProfileDto>
    {
        
        public ChangeUserNameHandler(DbContext dbContext, UserManager<User> userManager) : base(userManager, dbContext)
        {
        }

        public override async Task<UserProfileDto> Handle(ChangeUserNameCommand command, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);
            await UserManager.SetUserNameAsync(user, command.NewUserName);

            return new UserProfileDto(user.ProfileImageUrl,user.UserName, user.Email);
        }
    }
}