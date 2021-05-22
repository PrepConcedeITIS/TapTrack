using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Force.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
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
            var (entityType, projectId, entityId, userId) = request;
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(member => member.ProjectId == projectId)
                .SingleAsync(member => member.UserId == userId, cancellationToken);
            var comments = await DbContext
                .Set<Comment>()
                .WhereIf(entityType == "Issue", comment => comment.IssueId == entityId,
                    comment => comment.ArticleId == entityId)
                .WhereIf(teamMember.Role == "User", comment => !comment.IsDeleted, comment => true)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            comments.ForEach(dto => dto.IsEditable = dto.IsDeletable = dto.Author.UserId == userId);
            return comments;
        }
    }
}