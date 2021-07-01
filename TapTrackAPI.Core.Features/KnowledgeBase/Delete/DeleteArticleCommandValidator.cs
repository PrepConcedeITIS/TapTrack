using System;
using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Delete
{
    public class DeleteArticleCommandValidator : AbstractValidator<DeleteArticleCommand>
    {
        public DeleteArticleCommandValidator(DbContext dbContext)
        {
            var projectId = Guid.Empty;
            var articleId = Guid.Empty;
            RuleFor(x => x.Id)
                .MustAsync(async (x, token) =>
                {
                    articleId = x;
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
                                    var article = await dbContext
                                        .Set<Article>()
                                        .FindAsync(new object[] {articleId}, token);
                                    return article.CreatedById == teamMember.Id;
                                })
                                .WithMessage("Current user is neither project admin nor article creator")
                                .WithErrorCode("403");
                        });
                });
        }
    }
}