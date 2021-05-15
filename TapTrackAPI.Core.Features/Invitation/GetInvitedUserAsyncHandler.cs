using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Project;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class GetInvitedUserAsyncHandler : BaseCommandHandler,
        IRequestHandler<GetInvitedUserQuery, List<InvitationDto>>
    {
        public GetInvitedUserAsyncHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Task<List<InvitationDto>> Handle(GetInvitedUserQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(DbContext.Set<Entities.Invitation>().Where(x => x.Project.Id == request.ProjectId)
                .ProjectTo<InvitationDto>(Mapper.ConfigurationProvider).ToList());
        }
    }
}