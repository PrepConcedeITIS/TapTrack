using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Base.Handlers
{
    public abstract class BaseHandlerWithUserManager<TInput, TResult> : BaseHandler<TInput, TResult>
        where TInput : IRequest<TResult>
    {
        protected UserManager<User> UserManager { get; }

        protected BaseHandlerWithUserManager(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper)
        {
            UserManager = userManager;
        }
    }

    public abstract class BaseHandlerWithUserManager<TInput> : BaseHandler<TInput> where TInput : IRequest
    {
        protected UserManager<User> UserManager { get; }

        protected BaseHandlerWithUserManager(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper)
        {
            UserManager = userManager;
        }
    }
}