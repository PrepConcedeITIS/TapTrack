using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Restoration.PasswordReset
{
    [UsedImplicitly]
    public class PasswordResetHandler : RequestHandlerBase, IRequestHandler<PasswordResetCommand, Unit?>
    {
        private readonly UserManager<User> _userManager;

        public PasswordResetHandler(UserManager<User> userManager, DbContext dbContext, IMapper mapper)
            : base(dbContext, mapper)
        {
            _userManager = userManager;
        }

        public async Task<Unit?> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
        {
            var dbCode = Context.Set<RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode == null || dbCode.Code != userCode || DateTime.Compare(dbCode.ExpirationDate, DateTime.Now) <= 0)
                return null;

            dbCode.CodeIsUsed();
            var user = await _userManager.FindByEmailAsync(request.UserMail);
            if (user == null) return default;
            var result = await _userManager.RemovePasswordAsync(user);
            if (!result.Succeeded) return default;
            result = await _userManager.AddPasswordAsync(user, request.UserPassword);
            if (!result.Succeeded) return default;
            await Context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}