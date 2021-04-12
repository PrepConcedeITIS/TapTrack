using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project.Handlers
{
    public class GetProjectsListAsyncHandler : IRequestHandler<GetProjectsListQuery, List<ProjectDto>>
    {

        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public GetProjectsListAsyncHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<ProjectDto>> Handle(GetProjectsListQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Set<Entities.Project>()
                .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
    }
}
