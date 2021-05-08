using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Base;
using TapTrackAPI.Core.Features.Commenting.DTOs;
using TapTrackAPI.Core.Features.Commenting.Queries;

namespace TapTrackAPI.Core.Features.Commenting.Handlers
{
    public class GetAllEntityCommentsQueryHandler : BaseQueryHandler,
        IRequestHandler<GetAllEntityCommentsQuery, List<CommentDTO>>
    {
        public GetAllEntityCommentsQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<List<CommentDTO>> Handle(GetAllEntityCommentsQuery request,
            CancellationToken cancellationToken)
        {
            var (entityId, entityType) = request;
            return entityType switch
            {
                "Issue" => await GetIssueComments(entityId, cancellationToken),
                "Article" => await GetArticleComments(entityId, cancellationToken),
                _ => null
            };
        }

        private async Task<List<CommentDTO>> GetIssueComments(Guid entityId, CancellationToken token)
        {
            return await DbContext
                .Set<Comment>()
                .Where(comment => comment.IssueId == entityId)
                .OrderByDescending(comment => comment.LastUpdated)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(token);
        }

        private async Task<List<CommentDTO>> GetArticleComments(Guid entityId, CancellationToken token)
        {
            return await DbContext
                .Set<Comment>()
                .Where(comment => comment.ArticleId == entityId)
                .OrderByDescending(comment => comment.LastUpdated)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(token);
        }
    }
}