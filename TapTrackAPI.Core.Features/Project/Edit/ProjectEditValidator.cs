using FluentValidation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Project.Validators;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    [UsedImplicitly]
    public class ProjectEditValidator : ProjectValidatorBase<ProjectEditCommand>
    {
        public ProjectEditValidator(DbContext dbContext, UserManager<User> userManager) : base()
        {
            RuleFor(x => x)
                .MustAsync(async (command, token) =>
                {
                    var already = await dbContext.Set<Entities.Project>()
                        .FirstOrDefaultAsync(x => x.IdVisible == command.IdVisible && x.Id != command.ProjectId,
                            cancellationToken: token);
                    return already == null;
                })
                .WithMessage("Project with the same shortcut name already exist");
            RuleFor(x => x.ClaimsPrincipal)
                .SetAsyncValidator(new HasAccessToProjectPropertyValidator<ProjectEditCommand>(dbContext, userManager))
                .WithErrorCode("403")
                .WithMessage("You can't touch this project");
        }
    }

    public class ProjectValidatorBase<TProjectCommand> : AbstractValidator<TProjectCommand>
        where TProjectCommand : ProjectCommandBase
    {
        public ProjectValidatorBase()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30)
                .WithMessage("Length for project name is 1-30 characters");
            RuleFor(x => x.IdVisible).NotEmpty().MaximumLength(7)
                .WithMessage("Length for project shortcut name is 1-7 characters");
            RuleFor(x => x.Description).MaximumLength(500)
                .WithMessage("Max length for project description is 500 characters");
        }
    }
}