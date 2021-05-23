using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Commenting.Base;

namespace TapTrackAPI.Core.Features.Invitation.GetInvitationResults
{
    [UsedImplicitly]
    public class GetInvitationResultsAsyncHandler : BaseCommandHandler,
        IRequestHandler<GetInvitationResultsQuery, List<InvitationDto>>
    {
        public GetInvitationResultsAsyncHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<List<InvitationDto>> Handle(GetInvitationResultsQuery request, CancellationToken cancellationToken)
        {
            var result = await DbContext.Set<Entities.Invitation>()
                .Where(x => x.Project.Id == request.ProjectId)
                .ProjectTo<InvitationDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}