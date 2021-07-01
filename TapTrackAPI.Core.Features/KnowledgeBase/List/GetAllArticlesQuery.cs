using System;
using System.Collections.Generic;
using MediatR;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;

namespace TapTrackAPI.Core.Features.KnowledgeBase.List
{
    public record GetAllArticlesQuery(Guid UserId) : IRequest<List<ProjectWithArticlesDto>>;
}