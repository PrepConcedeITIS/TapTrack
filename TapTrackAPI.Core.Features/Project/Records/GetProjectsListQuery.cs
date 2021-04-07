using Force.Cqrs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Features.Project.Records
{
    public class GetProjectsListQuery : IRequest<List<ProjectDto>>
    {

    }
}
