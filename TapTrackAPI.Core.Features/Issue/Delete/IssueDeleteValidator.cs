using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Extensions;

namespace TapTrackAPI.Core.Features.Issue.Delete
{
    [UsedImplicitly]
    public class IssueDeleteValidator : AbstractValidator<IssueDeleteCommand>
    {
        public IssueDeleteValidator(DbContext dbContext, UserManager<User> userManager)
        {
            RuleFor(command => new {command.ClaimsPrincipal, command.IssueId})
                .MustAsync(async (commandData, ct) =>
                {
                    var userId = userManager.GetUserIdGuid(commandData.ClaimsPrincipal);
                    var teamMemberInfo = await dbContext.Set<Entities.Issue>()
                        .Where(x => x.Id == commandData.IssueId)
                        .Select(x => x.Project.Team
                            .Select(member => new {member.UserId, member.Role})
                            .FirstOrDefault(member => member.UserId == userId)
                        )
                        .FirstOrDefaultAsync(ct);
                    if (teamMemberInfo == null)
                        return false;

                    return teamMemberInfo.Role == Role.Admin.ToString();
                })
                .WithMessage("Only project admin can delete issues")
                .WithErrorCode("403");
        }
    }
}