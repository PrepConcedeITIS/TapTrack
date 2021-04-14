using JetBrains.Annotations;
using TapTrackAPI.Core.Features.Project.Edit;

namespace TapTrackAPI.Core.Features.Project.Create
{
    [UsedImplicitly]
    public class ProjectCreateValidator: ProjectValidatorBase<ProjectCreateCommand>
    {
        public ProjectCreateValidator(): base()
        {
        }
    }
}