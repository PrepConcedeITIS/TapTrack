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

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser
{
    [UsedImplicitly]
    public class GetInvitationsByUserAsyncHandler
        : BaseHandlerWithUserManager<GetInvitationsByUserQuery, InvitationDto[]>
    {
        public GetInvitationsByUserAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<InvitationDto[]> Handle(GetInvitationsByUserQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);
            var unResolvedInvitations = await DbContext.Set<Entities.Invitation>()
                .Where(invitation => invitation.InvitationState == InvitationState.Wait
                                     && invitation.UserId == userId)
                .ProjectTo<InvitationDto>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
            return unResolvedInvitations;
        }
    }
}