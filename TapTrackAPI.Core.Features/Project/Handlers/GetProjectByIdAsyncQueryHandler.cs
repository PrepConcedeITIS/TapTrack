using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project.Handlers
{
    public class GetProjectByIdAsyncQueryHandler : IAsyncQueryHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProjectByIdAsyncQueryHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery input)
        {
            var result = await _dbContext.Set<Entities.Project>()
                .Where(x => x.Id == input.ProjectId)
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return result;
        }
    }
}