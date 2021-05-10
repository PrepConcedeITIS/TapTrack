using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Profile.DeleteTelegramConnection
{
    [UsedImplicitly]
    public class DeleteTelegramConnectionAsyncCommandHandler
        : RequestHandlerBase, IRequestHandler<DeleteTelegramConnectionCommand, Unit?>
    {
        private readonly UserManager<User> _userManager;

        public DeleteTelegramConnectionAsyncCommandHandler(DbContext context, IMapper mapper,
            UserManager<User> userManager)
            : base(context, mapper)
        {
            _userManager = userManager;
        }

        public async Task<Unit?> Handle(DeleteTelegramConnectionCommand request, CancellationToken cancellationToken)
        {
            var userId = _userManager.GetUserIdGuid(request.ClaimsPrincipal);

            var tgConnection = await Context.Set<TelegramConnection>()
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
            if (tgConnection == default)
                return null;

            Context.Set<TelegramConnection>().Remove(tgConnection);
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}