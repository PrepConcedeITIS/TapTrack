using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Base.Handlers
{
    public interface IAsyncQueryHandler<in TQuery, TOut> : IQueryHandler<TQuery, Task<TOut>>
        where TQuery : IQuery<Task<TOut>>
    {
    }
}