using JetBrains.Annotations;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Mappers
{
    [UsedImplicitly]
    public class CommentingProfile : AutoMapper.Profile
    {
        public CommentingProfile()
        {
            CreateMap<Comment, CommentDTO>();
        }
    }
}