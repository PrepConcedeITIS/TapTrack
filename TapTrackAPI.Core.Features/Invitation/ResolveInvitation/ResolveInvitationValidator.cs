using System;
using System.Net;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Invitation.ResolveInvitation
{
    [UsedImplicitly]
    public class ResolveInvitationValidator : AbstractValidator<ResolveInvitationCommand>
    {
        public ResolveInvitationValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.Id)
                .Must(c => Guid.TryParse(c, out _))
                .WithErrorCode(HttpStatusCode.UnprocessableEntity)
                .WithMessage("Unable to resolve invitation");

            RuleFor(x => x)
                .MustAsync(async (command, ct) =>
                {
                    var invitationId = Guid.Parse(command.Id);
                    return await dbContext.Set<Entities.Invitation>()
                        .AnyAsync(x => x.Id == invitationId, ct);
                })
                .WithErrorCode(HttpStatusCode.NotFound)
                .WithMessage("Unable to find your invitation");

            RuleFor(x => x)
                .MustAsync(async (command, ct) =>
                {
                    var currentUserId = userManager.GetUserIdGuid(command.ClaimsPrincipal);
                    var invitationId = Guid.Parse(command.Id);
                    return (await dbContext.Set<Entities.Invitation>()
                        .FirstOrDefaultAsync(x => x.Id == invitationId, ct)).UserId == currentUserId;
                })
                .WithErrorCode(HttpStatusCode.Forbidden)
                .WithMessage("You don't touch this invitation");
        }
    }
}