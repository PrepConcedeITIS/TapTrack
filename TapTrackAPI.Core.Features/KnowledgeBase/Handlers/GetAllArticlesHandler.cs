using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.KnowledgeBase.Dtos;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class GetAllArticlesHandler : RequestHandlerBase,
        IRequestHandler<GetAllArticlesQuery, List<ProjectWithArticlesDto>>
    {
        public GetAllArticlesHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<ProjectWithArticlesDto>> Handle(GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var projectsWithArticles = await Context
                .Set<Entities.Project>()
                .ProjectTo<ProjectWithArticlesDto>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return projectsWithArticles;
        }
    }
}