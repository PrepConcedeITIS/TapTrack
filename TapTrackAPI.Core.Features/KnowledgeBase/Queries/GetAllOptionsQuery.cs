using System.Collections.Generic;
using System.Security.Claims;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Queries
{
    public record GetAllOptionsQuery(ClaimsPrincipal AppUser) : IRequest<List<OptionDto>>;
}