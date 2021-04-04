using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.CQRS;

namespace TapTrackAPI.Core.Features.Profile.Handlers
{
    public class ChangeUserNameHandler : ProfileHandlerWithDbContextBase<ChangeUserNameCommand, bool>
    {
        
        public ChangeUserNameHandler(DbContext dbContext, UserManager<User> userManager) : base(userManager, dbContext)
        {
        }

        public override async Task<bool> Handle(ChangeUserNameCommand command)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            if (string.IsNullOrEmpty(command.NewUserName) || string.IsNullOrWhiteSpace(command.NewUserName) ||
                command.NewUserName.Length > 25)
                return false; //уберу этот позор когда разберусь с валидаторами

            user.UserName = command.NewUserName;
            DbContext.Set<User>().Update(user);
            await DbContext.SaveChangesAsync();

            return true; //уберу этот позор когда разберусь с валидаторами
        }
    }
}