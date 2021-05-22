using FluentValidation;
using JetBrains.Annotations;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditIssueValidator: AbstractValidator<EditIssueCommand>
    {
        public EditIssueValidator()
        {
            RuleFor(x => x.Title).NotEmpty()
                .WithMessage("Issue name can't be empty")
                .MaximumLength(500)
                .WithMessage("Length for issue name is 1-500 characters");
        }
    }
}