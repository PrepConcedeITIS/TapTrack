using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.KnowledgeBase.ById
{
    [UsedImplicitly]
    public class GetArticleByIdQueryHandler : BaseHandler<GetArticleByIdQuery, FullArticleDto>
    {
        public GetArticleByIdQueryHandler(DbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override async Task<FullArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await DbContext
                .Set<Article>()
                .ProjectTo<FullArticleDto>(Mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            return article;
        }
    }
}