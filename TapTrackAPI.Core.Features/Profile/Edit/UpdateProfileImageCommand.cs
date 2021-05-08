using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Profile.Dto;

namespace TapTrackAPI.Core.Features.Profile.Edit
{
    public class UpdateProfileImageCommand : IRequest<UserProfileDto>
    {
        public IFormFile Image { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}