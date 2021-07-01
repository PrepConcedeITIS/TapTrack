using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class ChangeNotificationOptionHandler : BaseHandlerWithUserManager<ChangeNotificationOptionsCommand, bool>
    {
        public ChangeNotificationOptionHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager) :
            base(dbContext, mapper, userManager)
        {
        }

        public override async Task<bool> Handle(ChangeNotificationOptionsCommand command,
            CancellationToken cancellationToken)
        {
            var user = await UserManager.GetUserAsync(command.ClaimsPrincipal);

            var tgConnection = await DbContext.Set<TelegramConnection>()
                .Where(x => x.User.Id == user.Id)
                .FirstOrDefaultAsync(cancellationToken);


            if (tgConnection == null)
                return false;

            tgConnection.ChangeNotificationOption();
            var entityEntry = DbContext.Set<TelegramConnection>().Update(tgConnection);
            await DbContext.SaveChangesAsync(cancellationToken);

            return entityEntry.Entity.IsNotificationsEnabled;
        }
    }
}