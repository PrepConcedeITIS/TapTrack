using AutoMapper;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.DTOs;

namespace TapTrackAPI.Core.Features.Commenting.Mappers
{
    public class CommentingProfile : Profile
    {
        public CommentingProfile()
        {
            CreateMap<Comment, CommentDTO>();
        }
    }
}