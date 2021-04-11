using MediatR;
using System.Collections.Generic;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public class GetProjectsListQuery : IRequest<List<ProjectDto>>
    {

    }
}
