using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Project;

namespace TapTrackAPI.Core.Features.Invitation.ResolveInvitation
{
    [UsedImplicitly]
    public class ResolveInvitationAsyncHandler : BaseHandler<ResolveInvitationCommand, ProjectDto>
    {
        public ResolveInvitationAsyncHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<ProjectDto> Handle(ResolveInvitationCommand request, CancellationToken cancellationToken)
        {
            var invite = await DbContext.Set<Entities.Invitation>().Include(x => x.Project).Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.InvitationId), cancellationToken);

            invite.SetAcceptState(request.IsAccept);

            if (request.IsAccept)
            {
                var teamMember = new TeamMember(invite.UserId, invite.ProjectId, invite.InvitationRole);
                await DbContext.AddAsync(teamMember, cancellationToken);
            }

            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<ProjectDto>(invite.Project);
        }
    }
}