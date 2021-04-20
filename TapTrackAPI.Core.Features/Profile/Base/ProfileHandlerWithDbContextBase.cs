using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Profile.Base
{
    public abstract class ProfileHandlerWithDbContextBase<TIn,TOut> : ProfileHandlerBase<TIn,TOut> where TIn : IRequest<TOut>
    {
        protected readonly DbContext DbContext;

        protected ProfileHandlerWithDbContextBase(UserManager<User> userManager, DbContext dbContext) : base(userManager)
        {
            DbContext = dbContext;
        }
    }
}