using FluentValidation;
using JetBrains.Annotations;

namespace TapTrackAPI.Core.Features.Project.Create
{
    [UsedImplicitly]
    public class ProjectCreateValidator: AbstractValidator<ProjectCreateCommand>
    {
        public ProjectCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30);
            RuleFor(x => x.IdVisible).NotEmpty().MaximumLength(7);
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}