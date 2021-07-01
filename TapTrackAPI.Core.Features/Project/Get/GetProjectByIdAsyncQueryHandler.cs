using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Project.Get
{
    [UsedImplicitly]
    public class GetProjectByIdAsyncQueryHandler : BaseHandler<GetProjectByIdQuery, ProjectDto>
    {
        public GetProjectByIdAsyncQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<ProjectDto> Handle(GetProjectByIdQuery input, CancellationToken cancellationToken)
        {
            var result = await DbContext.Set<Entities.Project>()
                .Where(x => x.Id == input.ProjectId)
                .ProjectTo<ProjectDto>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}