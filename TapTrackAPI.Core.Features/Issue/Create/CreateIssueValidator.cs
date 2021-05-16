using FluentValidation;
using JetBrains.Annotations;

namespace TapTrackAPI.Core.Features.Issue.Create
{
    [UsedImplicitly]
    public class CreateIssueValidator: AbstractValidator<CreateIssueCommand>
    {
        public CreateIssueValidator(): base()
        {
        }
    }
}