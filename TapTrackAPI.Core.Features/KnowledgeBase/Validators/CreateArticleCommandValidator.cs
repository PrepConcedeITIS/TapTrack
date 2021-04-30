using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;

namespace TapTrackAPI.Core.Features.KnowledgeBase.Validators
{
    public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
    {
        public CreateArticleCommandValidator(DbContext context, UserManager<User> userManager)
        {
            Guid id = default;
            RuleFor(command => command.BelongsToId)
                .MustAsync(async (guid, token) =>
                {
                    var project = await context
                        .Set<Entities.Project>()
                        .SingleOrDefaultAsync(x => x.Id == guid, token);
                    if (project == null)
                        return false;
                    id = project.Id;
                    return true;
                })
                .WithMessage("Project with this id doesn't exist")
                .DependentRules(() =>
                {
                    RuleFor(command => command.AppUser)
                        .MustAsync(async (principal, token) =>
                        {
                            var userId = userManager.GetUserIdGuid(principal);
                            return await context
                                .Set<TeamMember>()
                                .Where(x => x.ProjectId == id)
                                .AnyAsync(member => member.UserId == userId, token);
                        })
                        .WithMessage("Current user isn't member of this project")
                        .WithErrorCode("403");
                    RuleFor(command => command.Title).NotEmpty();
                    RuleFor(command => command.Content).NotEmpty();
                });
        }
    }
}