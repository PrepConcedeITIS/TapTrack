using System;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.Project.Base;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Delete
{
    public record ProjectDeleteCommand(Guid ProjectId) : IRequest, IHasClaims, IHasProjectId
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}