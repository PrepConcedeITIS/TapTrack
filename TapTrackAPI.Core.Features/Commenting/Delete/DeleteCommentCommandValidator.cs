using System.Linq;
using FluentValidation;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Commenting.Delete
{
    [UsedImplicitly]
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator(DbContext dbContext)
        {
            var comments = dbContext.Set<Comment>();
            RuleFor(command => command.Id)
                .MustAsync(async (id, token) => await comments.AnyAsync(comment => comment.Id == id, token))
                .WithMessage("Comment with this id doesn't exist")
                .DependentRules(() =>
                {
                    RuleFor(command => new {command.IsCommentBeingDeletedPermanently, command.ProjectId, command.UserId})
                        .MustAsync(async (x, token) =>
                        {
                            if (!x.IsCommentBeingDeletedPermanently)
                                return await comments.AnyAsync(comment => comment.Author.UserId == x.UserId, token);
                            var teamMember = await dbContext
                                .Set<TeamMember>()
                                .Where(member => member.ProjectId == x.ProjectId)
                                .SingleOrDefaultAsync(member => member.UserId == x.UserId, token);
                            if (teamMember == null)
                                return false;
                            return teamMember.Role == "Admin";
                        })
                        .WithMessage("Current user neither author of this comment nor admin of project")
                        .WithErrorCode("403");
                });
        }
    }
}