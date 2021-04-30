using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Validators
{
    public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
    {
        public UpdateArticleCommandValidator(DbContext dbContext)
        {
            var projectId = Guid.Empty;
            RuleFor(x => x.Id)
                .MustAsync(async (x, token) =>
                {
                    return await dbContext
                        .Set<Article>()
                        .AnyAsync(y => y.Id == x, token);
                })
                .WithMessage("Article with this id doesn't exist")
                .DependentRules(() =>
                {
                    RuleFor(x => x.BelongsToId)
                        .MustAsync(async (x, token) =>
                        {
                            projectId = x;
                            return await dbContext
                                .Set<Article>()
                                .AnyAsync(y => y.BelongsToId == x, token);
                        })
                        .WithMessage("Project with this id doesn't exist")
                        .DependentRules(() =>
                        {
                            RuleFor(x => x.UserId)
                                .MustAsync(async (x, token) =>
                                {
                                    return await dbContext
                                        .Set<TeamMember>()
                                        .Where(y => y.ProjectId == projectId)
                                        .AnyAsync(y => y.UserId == x, token);
                                })
                                .WithMessage("Current user isn't member of this project")
                                .WithErrorCode("403");
                            RuleFor(x => x.Title).NotEmpty();
                            RuleFor(x => x.Content).NotEmpty();
                        });
                });
        }
    }
}