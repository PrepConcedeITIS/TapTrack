using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Restoration.PasswordReset
{
    [UsedImplicitly]
    public class PasswordResetHandler : BaseHandlerWithUserManager<PasswordResetCommand, Unit?>
    {
        public PasswordResetHandler(DbContext dbContext, IMapper mapper, UserManager<User> userManager)
            : base(dbContext, mapper, userManager)
        {
        }

        public override async Task<Unit?> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            var dbCode = DbContext.Set<RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode == null || dbCode.Code != userCode || DateTime.Compare(dbCode.ExpirationDate, DateTime.UtcNow) <= 0)
                return null;

            dbCode.CodeIsUsed();
            var user = await UserManager.FindByEmailAsync(request.UserMail);
            if (user == null) return default;
            var result = await UserManager.RemovePasswordAsync(user);
            if (!result.Succeeded) return default;
            result = await UserManager.AddPasswordAsync(user, request.UserPassword);
            if (!result.Succeeded) return default;
            await DbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}