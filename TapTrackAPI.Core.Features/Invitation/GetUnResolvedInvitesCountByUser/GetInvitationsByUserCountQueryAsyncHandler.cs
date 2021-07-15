using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser;

namespace TapTrackAPI.Core.Features.Invitation.GetUnResolvedInvitesCountByUser
{
    [UsedImplicitly]
    public class GetInvitationsByUserAsyncHandler
        : BaseHandlerWithUserManager<GetInvitationsByUserCountQuery, int>
    {
        public GetInvitationsByUserAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<int> Handle(GetInvitationsByUserCountQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);
            var unResolvedInvitationsCount = await DbContext.Set<Entities.Invitation>()
                .Where(invitation => invitation.InvitationState == InvitationState.Wait
                                     && invitation.UserId == userId)
                .CountAsync(cancellationToken);
            return unResolvedInvitationsCount;
        }
    }
}