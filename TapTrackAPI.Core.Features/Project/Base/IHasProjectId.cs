using System;

namespace TapTrackAPI.Core.Features.Project.Base
{
    public interface IHasProjectId
    {
        public Guid ProjectId { get; init; }
    }
}