using JetBrains.Annotations;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Update;

namespace TapTrackAPI.Core.Features.Commenting
{
    [UsedImplicitly]
    public class CommentingProfile : AutoMapper.Profile
    {
        public CommentingProfile()
        {
            CreateMap<Comment, CommentDTO>();
            CreateMap<Comment, EditedCommentDTO>();
        }
    }
}