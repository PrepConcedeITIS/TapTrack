using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Commands;

namespace TapTrackAPI.Core.Features.Commenting.Validators
{
    public class RestoreCommentCommandValidator : AbstractValidator<RestoreCommentCommand>
    {
        public RestoreCommentCommandValidator(DbContext dbContext)
        {
            Comment comment = null;
            RuleFor(command => command.Id)
                .MustAsync(async (id, token) =>
                {
                    comment = await dbContext
                        .Set<Comment>()
                        .FindAsync(new object[] {id}, token);
                    return comment is {IsDeleted: true};
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
                            return comment.IsDeleted && teamMember.Role == "Admin";
                        })
                        .WithMessage("Current user isn't admin of this project")
                        .WithErrorCode("403");
                });
        }
    }
}