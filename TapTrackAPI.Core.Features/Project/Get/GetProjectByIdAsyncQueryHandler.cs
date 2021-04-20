using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Features.Project.Get
{
    [UsedImplicitly]
    public class GetProjectByIdAsyncQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProjectByIdAsyncQueryHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery input, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Set<Entities.Project>()
                .Where(x => x.Id == input.ProjectId)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            return result;
        }
    }
}