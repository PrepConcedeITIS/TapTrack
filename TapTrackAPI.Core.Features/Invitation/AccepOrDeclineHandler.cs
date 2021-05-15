using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Project;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class AcceptOrDeclineHandler : BaseCommandHandler, IRequestHandler<AcceptOrDeclineCommand, ProjectDto>
    {
        public AcceptOrDeclineHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<ProjectDto> Handle(AcceptOrDeclineCommand request, CancellationToken cancellationToken)
        {
            var invite = await DbContext.Set<Entities.Invitation>().Include(x => x.Project).Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == request.InvitationId, cancellationToken: cancellationToken);

            invite.SetAcceptState(request.IsAccept);
            await DbContext.SaveChangesAsync(cancellationToken);
            return Mapper.Map<ProjectDto>(invite.Project);
        }
    }
}