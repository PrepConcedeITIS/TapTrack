using System.Linq;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Commenting.Queries;

namespace TapTrackAPI.Core.Features.Commenting.Validators
{
    public class GetAllEntityCommentsQueryValidator : AbstractValidator<GetAllEntityCommentsQuery>
    {
        public GetAllEntityCommentsQueryValidator(DbContext dbContext)
        {
            RuleFor(command => new {command.EntityType, command.ProjectId, command.EntityId})
                .MustAsync(async (x, token) =>
                {
                    return x.EntityType switch
                    {
                        "Issue" => await dbContext.Set<Entities.Issue>()
                            .Where(issue => issue.ProjectId == x.ProjectId)
                            .AnyAsync(issue => issue.Id == x.EntityId, token),
                        "Article" => await dbContext.Set<Article>()
                            .Where(article => article.BelongsToId == x.ProjectId)
                            .AnyAsync(article => article.Id == x.EntityId, token),
                        _ => false
                    };
                })
                .WithMessage("Entity type is wrong or project with this entity doesn't exist")
                .DependentRules(() =>
                {
                    RuleFor(command => new {command.ProjectId, command.UserId})
                        .MustAsync(async (x, token) =>
                        {
                            return await dbContext
                                .Set<TeamMember>()
                                .Where(member => member.ProjectId == x.ProjectId)
                                .AnyAsync(member => member.UserId == x.UserId, token);
                        })
                        .WithMessage("Current user isn't member of this project")
                        .WithErrorCode("403");
                });
        }
    }
}