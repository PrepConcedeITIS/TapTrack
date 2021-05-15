using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InviteUserValidator : AbstractValidator<InviteUserCommand>
    {
        public InviteUserValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(x => x.Email)
                .Must((command, _) => dbContext.Set<User>().FirstOrDefault(x => x.Email == command.Email) != null)
                .WithMessage("Пользователь с таким email не найден")
                .WithErrorCode("422");
            RuleFor(x => x.ProjectId)
                .Must((command, _) =>
                    dbContext.Set<Entities.Project>().FirstOrDefault(c => c.Id == command.ProjectId) != null)
                .WithMessage("Проект не найден")
                .WithErrorCode("422");
            RuleFor(x => x)
                .Must((command, _) =>
                    dbContext.Set<Entities.Invitation>().FirstOrDefault(c =>
                        c.Id == command.ProjectId && c.User.Email == command.Email &&
                        c.InvitationState == InvitationState.Wait) == null)
                .WithMessage("Приглашение пользователю уже отправлено")
                .WithErrorCode("422");
        }
    }
}