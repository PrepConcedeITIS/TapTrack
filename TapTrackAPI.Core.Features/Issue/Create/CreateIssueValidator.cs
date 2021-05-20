using FluentValidation;
using JetBrains.Annotations;

namespace TapTrackAPI.Core.Features.Issue.Create
{
    [UsedImplicitly]
    public class CreateIssueValidator: AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueValidator(): base()
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Issue name can't be empty")
                .MaximumLength(500)
                .WithMessage("Length for issue name is 1-500 characters");
        }
    }
}