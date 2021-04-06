using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public class GetListIssueQuery : IRequest<List<IssueListDto>>
    {
        
    }
}