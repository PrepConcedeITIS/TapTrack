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
                .Must((command, _) => dbContext.Set<User>().FirstOrDefault(x => x.Email == command.Email) != null)
                .WithMessage("User not found")
                .WithErrorCode("422");
            RuleFor(x => x.ProjectId)
                .Must((command, _) =>
                    dbContext.Set<Entities.Project>().FirstOrDefault(c => c.Id == command.ProjectId) != null)
                .WithMessage("Project not found")
                .WithErrorCode("422");
            RuleFor(x => x)
                .Must((command, _) =>
                    dbContext.Set<Entities.Invitation>().FirstOrDefault(c =>
                        c.Id == command.ProjectId && c.User.Email == command.Email &&
                        c.InvitationState == InvitationState.Wait) == null)
                .WithMessage("Invitation to user already send")
                .WithErrorCode("422");
        }
    }
}