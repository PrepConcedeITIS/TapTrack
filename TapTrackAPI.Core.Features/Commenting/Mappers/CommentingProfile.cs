using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Mappers
{
    [UsedImplicitly]
    public class CommentingProfile : AutoMapper.Profile
    {
        public CommentingProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dto => dto.ProjectId,
                    expression => expression.MapFrom(comment => comment.Article.BelongsToId));
            CreateMap<Comment, EditedCommentDTO>();
        }
    }
}