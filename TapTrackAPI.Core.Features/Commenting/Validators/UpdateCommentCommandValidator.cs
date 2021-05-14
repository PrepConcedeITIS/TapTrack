using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Commands;

namespace TapTrackAPI.Core.Features.Commenting.Validators
{
    public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
    {
        public UpdateCommentCommandValidator(DbContext dbContext)
        {
            var comments = dbContext.Set<Comment>(); 
            RuleFor(command => command.Id)
                .MustAsync(async (id, token) =>
                {
                    return await comments.AnyAsync(comment => comment.Id == id, token);
                })
                .WithMessage("Comment with this id doesn't exist")
                .DependentRules(() =>
                {
                    RuleFor(command => command.UserId)
                        .MustAsync(async (userId, token) =>
                        {
                            return await comments.AnyAsync(comment => comment.Author.UserId == userId, token);
                        })
                        .WithMessage("Current user isn't author of this comment")
                        .WithErrorCode("403")
                        .DependentRules(() =>
                        {
                            RuleFor(command => command.Text).NotEmpty();
                        });
                });
        }
    }
}