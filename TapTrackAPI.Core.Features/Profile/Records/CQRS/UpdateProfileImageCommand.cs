using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;
using Microsoft.AspNetCore.Http;
using TapTrackAPI.Core.Features.Profile.Base;
using TapTrackAPI.Core.Features.Profile.Records.Dtos;

namespace TapTrackAPI.Core.Features.Profile.Records.CQRS
{
    public class UpdateProfileImageCommand : IQuery<Task<UserProfileDto>>
    {
        public IFormFile Image { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}