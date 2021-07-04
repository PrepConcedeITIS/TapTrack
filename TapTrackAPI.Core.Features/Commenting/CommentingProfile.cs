using JetBrains.Annotations;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Update;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.Commenting
{
    [UsedImplicitly]
    public class CommentingProfile : AutoMapper.Profile
    {
        public CommentingProfile()
        {
            CreateMap<Comment, CommentDTO>()
                .ForMember(dist=>dist.Author, 
                    cfg=>cfg.MapFrom(source=>source.Author.User));
            CreateMap<Comment, EditedCommentDTO>();
        }

        private UserDto SetUser(Comment comment)
        {
            return new UserDto();
        }
    }
}