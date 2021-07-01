using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Restore
{
    [UsedImplicitly]
    public class RestoreCommentCommandValidator : AbstractValidator<RestoreCommentCommand>
    {
        public RestoreCommentCommandValidator(DbContext dbContext)
        {
            RuleFor(command => command.Id)
                .MustAsync(async (id, token) =>
                {
                    return await dbContext
                        .Set<Comment>()
                        .AnyAsync(comment => comment.Id == id && comment.IsDeleted, token);
                })
                .WithMessage("Comment with this id doesn't exist or wasn't really deleted")
                .DependentRules(() =>
                {
                    RuleFor(command => new { command.ProjectId, command.UserId})
                        .MustAsync(async (x, token) =>
                        {
                            var teamMember = await dbContext
                                .Set<TeamMember>()
                                .Where(member => member.ProjectId == x.ProjectId)
                                .SingleOrDefaultAsync(member => member.UserId == x.UserId, token);
                            if (teamMember == null)
                                return false;
                            return teamMember.Role == "Admin";
                        })
                        .WithMessage("Current user isn't admin of this project")
                        .WithErrorCode("403");
                });
        }
    }
}