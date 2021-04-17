using MediatR;

namespace TapTrackAPI.Core.Features.Project.IdVisibleUnique
{
    public record GetUniquenessOfIdQuery(string IdVisible) : IRequest<bool>;
}