using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public record RecordBase<T>(ClaimsPrincipal ClaimsPrincipal): IQuery<Task<T>>;
}