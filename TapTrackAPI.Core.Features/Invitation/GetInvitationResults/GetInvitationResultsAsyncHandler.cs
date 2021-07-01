using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    [UsedImplicitly]
    public class GetInvitationResultsAsyncHandler : BaseHandler<GetInvitationResultsQuery, List<InvitationDto>>
    {
        public GetInvitationResultsAsyncHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<List<InvitationDto>> Handle(GetInvitationResultsQuery request, CancellationToken cancellationToken)
        {
            var result = await DbContext.Set<Entities.Invitation>()
                .Where(x => x.Project.Id == request.ProjectId)
                .ProjectTo<InvitationDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}