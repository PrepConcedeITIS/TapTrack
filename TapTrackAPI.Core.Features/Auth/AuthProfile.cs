using AutoMapper;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Auth.Base;

namespace TapTrackAPI.Core.Features.Auth
{
    public class AuthProfile: Profile
    {
        public AuthProfile()
        {
            CreateMap<User, AuthUserDto>();
        }
    }
}