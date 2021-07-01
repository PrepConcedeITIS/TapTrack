using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class GetArticleByIdQueryHandler : RequestHandlerBase, IRequestHandler<GetArticleByIdQuery, FullArticleDto>
    {
        public GetArticleByIdQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<FullArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await Context
                .Set<Article>()
                .ProjectTo<FullArticleDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return article;
        }
    }
}