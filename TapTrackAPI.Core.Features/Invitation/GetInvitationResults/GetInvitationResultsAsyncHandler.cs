using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Invitation.Dto;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    [UsedImplicitly]
    public class GetInvitationResultsAsyncHandler : BaseHandler<GetInvitationResultsQuery, List<InvitationGridDto>>
    {
        public GetInvitationResultsAsyncHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<List<InvitationGridDto>> Handle(GetInvitationResultsQuery request,
            CancellationToken cancellationToken)
        {
            var result = await DbContext.Set<Entities.Invitation>()
                .Where(x => x.Project.Id == request.ProjectId)
                .ProjectTo<InvitationGridDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}