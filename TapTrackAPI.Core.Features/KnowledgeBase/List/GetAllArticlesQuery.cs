using System;
using System.Collections.Generic;
using MediatR;

namespace TapTrackAPI.Core.Features.KnowledgeBase.List
{
    public record GetAllArticlesQuery(Guid UserId) : IRequest<List<ProjectWithArticlesDto>>;
}