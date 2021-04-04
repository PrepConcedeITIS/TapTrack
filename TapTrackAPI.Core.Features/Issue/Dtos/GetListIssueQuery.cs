using System.Collections.Generic;
using System.Threading.Tasks;
using Force.Cqrs;

namespace TapTrackAPI.Core.Features.Issue.Dtos
{
    public class GetListIssueQuery : IQuery<Task<List<IssueListDto>>>
    {
        
    }
}