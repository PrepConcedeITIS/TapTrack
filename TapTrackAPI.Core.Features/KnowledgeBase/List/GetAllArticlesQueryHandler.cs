using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.KnowledgeBase.List
{
    [UsedImplicitly]
    public class GetAllArticlesQueryHandler : BaseHandler<GetAllArticlesQuery, List<ProjectWithArticlesDto>>
    {
        public GetAllArticlesQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<List<ProjectWithArticlesDto>> Handle(GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var projectsWithArticles = await DbContext
                .Set<Entities.Project>()
                .Where(project => project.Team.Any(member => member.UserId == request.UserId))
                .ProjectTo<ProjectWithArticlesDto>(Mapper.ConfigurationProvider)
                .Where(x => x.Articles.Any())
                .ToListAsync(cancellationToken);
            return projectsWithArticles;
        }
    }
}