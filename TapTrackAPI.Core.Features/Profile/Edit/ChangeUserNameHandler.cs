using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public class ChangeUserNameHandler : ProfileHandlerWithDbContextBase<ChangeUserNameCommand, UserProfileDto>
    {
        
        public ChangeUserNameHandler(DbContext dbContext, UserManager<User> userManager) : base(userManager, dbContext)
        {
        }

        public override async Task<UserProfileDto> Handle(ChangeUserNameCommand command, CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            if (string.IsNullOrEmpty(command.NewUserName) || string.IsNullOrWhiteSpace(command.NewUserName) ||
                command.NewUserName.Length > 25)
                return new UserProfileDto(null,null,null); //уберу когда разберусь с валидаторами

            user.UserName = command.NewUserName;
            DbContext.Set<User>().Update(user);
            await DbContext.SaveChangesAsync(cancellationToken);

            return new UserProfileDto(user.ProfileImageUrl,user.UserName, user.Email);
        }
    }
}