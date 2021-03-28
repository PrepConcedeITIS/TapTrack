using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public record GetUniquenessOfIdQuery(string IdVisible) : IQuery<Task<bool>>;
}