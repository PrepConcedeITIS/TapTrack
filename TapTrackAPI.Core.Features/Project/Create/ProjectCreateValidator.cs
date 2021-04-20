using JetBrains.Annotations;
using TapTrackAPI.Core.Features.Project.Base;

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