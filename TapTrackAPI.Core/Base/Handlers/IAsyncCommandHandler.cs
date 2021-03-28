using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Base.Handlers
{
    public interface IAsyncCommandHandler<in TIn, TOut> : ICommandHandler<TIn, Task<TOut>>
        where TIn : ICommand<Task<TOut>>
    {
    }
}