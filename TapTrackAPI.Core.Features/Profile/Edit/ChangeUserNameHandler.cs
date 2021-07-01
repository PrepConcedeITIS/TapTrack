using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class ChangeUserNameHandler : BaseHandlerWithUserManager<ChangeUserNameCommand, UserProfileDto>
    {
        public ChangeUserNameHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<UserProfileDto> Handle(ChangeUserNameCommand command,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);
            await UserManager.SetUserNameAsync(user, command.NewUserName);

            return new UserProfileDto(user.ProfileImageUrl, user.UserName, user.Email);
        }
    }
}