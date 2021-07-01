using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class GetAllArticlesQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllArticlesQuery, List<ProjectWithArticlesDto>>
    {
        public GetAllArticlesQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<ProjectWithArticlesDto>> Handle(GetAllArticlesQuery request,
            CancellationToken cancellationToken)
        {
            var projectsWithArticles = await Context
                .Set<Entities.Project>()
                .Where(project => project.Team.Any(member => member.UserId == request.UserId))
                .ProjectTo<ProjectWithArticlesDto>(Mapper.ConfigurationProvider)
                .Where(x=>x.Articles.Any())
                .ToListAsync(cancellationToken);
            return projectsWithArticles;
        }
    }
}