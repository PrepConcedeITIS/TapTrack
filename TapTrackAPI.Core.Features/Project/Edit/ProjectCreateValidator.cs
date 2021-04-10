using System.Security.Claims;
using FluentValidation;
using FluentValidation.Validators;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project.Edit
{
    public class ProjectEditValidator: AbstractValidator<ProjectCreateCommand>
    {
        public ProjectEditValidator()
        {
            RuleFor(x=>x.Claims).SetValidator(new )
        }
    }
    
    public class HasAccessPropertyValidator: PropertyValidator<CommandBase, ClaimsPrincipal>
    {
        public override bool IsValid(ValidationContext<CommandBase> context, ClaimsPrincipal value)
        {
            throw new System.NotImplementedException();
        }

        public override string Name { get; }
    }
}