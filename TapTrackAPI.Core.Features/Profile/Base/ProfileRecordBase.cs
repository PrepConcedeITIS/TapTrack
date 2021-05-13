using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public record ProfileRecordBase<T> : IRequest<T>, IHasClaims
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}