using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Validators
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(DbContext dbContext)
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
                                    var teamMember = await dbContext
                                        .Set<TeamMember>()
                                        .Where(y => y.ProjectId == projectId)
                                        .SingleOrDefaultAsync(y => y.UserId == x, token);
                                    if (teamMember == null)
                                        return false;
                                    if (teamMember.Role == "Admin")
                                        return true;
                                    return await dbContext
                                        .Set<Article>()
                                        .AnyAsync(y => y.CreatedById == teamMember.Id, token);
                                })
                                .WithMessage("Current user is neither project admin nor article creator")
                                .WithErrorCode("403");
                        });
                });
        }
    }
}