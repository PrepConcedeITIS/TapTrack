using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Base
{
    public class BaseCommandHandler
    {
        protected DbContext DbContext { get; }
        protected UserManager<User> UserManager { get; }

        public BaseCommandHandler(DbContext dbContext, UserManager<User> userManager)
        {
            DbContext = dbContext;
            UserManager = userManager;
        }
    }
}