using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Invitation.InviteUser
{
    [UsedImplicitly]
    public class InviteUserValidator : AbstractValidator<InviteUserCommand>
    {
        public InviteUserValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.Email)
                .MustAsync(async (command, _) => await userManager.FindByEmailAsync(command) != default)
                .WithMessage("User not found")
                .WithErrorCode("422");

            RuleFor(x => x.ProjectId)
                .Must((command, _) =>
                    dbContext.Set<Entities.Project>().FirstOrDefault(c => c.Id == command.ProjectId) != null)
                .WithMessage("Project not found")
                .WithErrorCode("422");

            RuleFor(x => x)
                .MustAsync(async (command, ct) =>
                    !await dbContext
                        .Set<Entities.Invitation>()
                        .Where(invitation => invitation.InvitationState == InvitationState.Wait)
                        .AnyAsync(invitation =>
                            invitation.User.NormalizedEmail == command.Email.ToUpperInvariant()
                            && invitation.ProjectId == command.ProjectId, ct))
                .WithMessage("Invitation to user already send")
                .WithErrorCode("422");
        }
    }
}