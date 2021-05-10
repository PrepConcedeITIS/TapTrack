using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Profile.Base;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    [UsedImplicitly]
    public class
        ChangeNotificationOptionHandler : ProfileHandlerWithDbContextBase<ChangeNotificationOptionsCommand, bool>
    {
        public ChangeNotificationOptionHandler(UserManager<User> userManager, DbContext dbContext) : base(userManager,
            dbContext)
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