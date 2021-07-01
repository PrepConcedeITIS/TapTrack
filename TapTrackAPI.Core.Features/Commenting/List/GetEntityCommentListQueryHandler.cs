using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Force.Linq;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.List
{
    [UsedImplicitly]
    public class GetEntityCommentListQueryHandler : BaseHandler<GetEntityCommentListQuery, List<CommentDTO>>
    {
        public GetEntityCommentListQueryHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<List<CommentDTO>> Handle(GetEntityCommentListQuery request,
            CancellationToken cancellationToken)
        {
            var (entityType, projectId, entityId, userId) = request;
            var teamMember = await DbContext
                .Set<TeamMember>()
                .Where(member => member.ProjectId == projectId)
                .SingleAsync(member => member.UserId == userId, cancellationToken);
            var comments = await DbContext
                .Set<Comment>()
                .WhereIf(entityType == "Issue", comment => comment.IssueId == entityId, comment => comment.ArticleId == entityId)
                .WhereIf(teamMember.Role == "User", comment => !comment.IsDeleted, comment => true)
                .OrderByDescending(comment => comment.Created)
                .ProjectTo<CommentDTO>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            comments.ForEach(dto => dto.IsEditable = dto.IsDeletable = dto.Author.UserId == userId);
            return comments;
        }
    }
}