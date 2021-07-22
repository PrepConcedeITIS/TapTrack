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
using TapTrackAPI.Core.Features.Invitation.Dto;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationsByUser
{
    [UsedImplicitly]
    public class GetInvitationsByUserAsyncHandler
        : BaseHandlerWithUserManager<GetInvitationsByUserQuery, InvitationDtoDetailed[]>
    {
        public GetInvitationsByUserAsyncHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<InvitationDtoDetailed[]> Handle(GetInvitationsByUserQuery request,
            CancellationToken cancellationToken)
        {
            var userId = UserManager.GetUserIdGuid(request.ClaimsPrincipal);
            var unResolvedInvitations = await DbContext.Set<Entities.Invitation>()
                .Where(invitation => invitation.InvitationState == InvitationState.Wait
                                     && invitation.UserId == userId)
                .ProjectTo<InvitationDtoDetailed>(Mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
            return unResolvedInvitations;
        }
    }
}