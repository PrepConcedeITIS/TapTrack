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
            var (entityId, entityType, userId) = request;
            return entityType switch
            {
                "Issue" => await GetIssueComments(entityId, userId, cancellationToken),
                "Article" => await GetArticleComments(entityId, userId, cancellationToken),
                _ => null
            };
        }

        private async Task<List<CommentDTO>> GetIssueComments(Guid entityId, Guid userId, CancellationToken token)
        {
            var comments = await DbContext
                .Set<Comment>()
                .Where(comment => !comment.IsDeleted && comment.IssueId == entityId)
                .OrderByDescending(comment => comment.Created)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(token);
            comments.ForEach(dto => dto.IsEditable = dto.Author.UserId == userId);
            return comments;
        }

        private async Task<List<CommentDTO>> GetArticleComments(Guid entityId, Guid userId, CancellationToken token)
        {
            var comments = await DbContext
                .Set<Comment>()
                .Where(comment => !comment.IsDeleted && comment.ArticleId == entityId)
                .OrderByDescending(comment => comment.Created)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(token);
            comments.ForEach(dto => dto.IsEditable = dto.Author.UserId == userId);
            return comments;
        }
    }
}