using FluentValidation;
using TapTrackAPI.Core.Features.Project.Edit;

namespace TapTrackAPI.Core.Features.Project.Base
{
    public class ProjectValidatorBase<TProjectCommand> : AbstractValidator<TProjectCommand>
        where TProjectCommand : ProjectCommandBase
    {
        public ProjectValidatorBase()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Project name can't be empty")
                .MaximumLength(30)
                .WithMessage("Length for project name is 1-30 characters");
            RuleFor(x => x.IdVisible).NotEmpty()
                .WithMessage("Project shortcut name can't be empty")
                .MaximumLength(7)
                .WithMessage("Length for project shortcut name is 1-7 characters");
            RuleFor(x => x.Description).MaximumLength(500)
                .WithMessage("Max length for project description is 500 characters");
        }
    }
}