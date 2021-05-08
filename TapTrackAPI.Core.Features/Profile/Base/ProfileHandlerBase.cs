using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public abstract class ProfileHandlerBase<TIn, TOut> :
        IRequestHandler<TIn, TOut> where TIn : IRequest<TOut>
    {
        protected readonly UserManager<User> UserManager;

        protected ProfileHandlerBase(UserManager<User> userManager)
        {
            UserManager = userManager;
        }

        public abstract Task<TOut> Handle(TIn request, CancellationToken cancellationToken);
    }
}