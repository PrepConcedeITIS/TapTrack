using JetBrains.Annotations;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth
{
    [UsedImplicitly]
    public class AuthProfile : AutoMapper.Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthUserDto>();
        }
    }
}