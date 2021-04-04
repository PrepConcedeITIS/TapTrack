using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.Dtos;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Handlers
{
    public class GetArticleByIdHandler : RequestHandlerBase, IRequestHandler<GetArticleByIdQuery, FullArticleDto>
    {
        public GetArticleByIdHandler(DbContext context, IMapper mapper) : base(context, mapper)
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