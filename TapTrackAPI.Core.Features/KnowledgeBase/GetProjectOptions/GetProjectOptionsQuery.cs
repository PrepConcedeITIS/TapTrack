using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.KnowledgeBase.GetProjectOptions
{
    public record GetProjectOptionsQuery(ClaimsPrincipal AppUser) : IRequest<List<OptionDto>>;
}