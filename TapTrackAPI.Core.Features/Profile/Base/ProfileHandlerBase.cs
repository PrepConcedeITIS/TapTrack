using System.Threading.Tasks;
using Force.Cqrs;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public abstract class ProfileHandlerBase<TIn,TOut> : 
        IAsyncQueryHandler<TIn,TOut> where TIn : IQuery<Task<TOut>>
    {
        protected readonly UserManager<User> UserManager;

        protected ProfileHandlerBase(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public abstract Task<TOut> Handle(TIn input);
    }
}