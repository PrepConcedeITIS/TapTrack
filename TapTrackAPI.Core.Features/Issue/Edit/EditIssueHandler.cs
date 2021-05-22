using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    // public class EditIssueHandler : RequestHandlerBase, IRequestHandler<EditIssueCommand, IssueDetailsDto>
    // {
    //     private readonly IMediator _mediator;
    //
    //     public EditIssueHandler(DbContext dbContext, IMediator mediator, IMapper mapper) : base(dbContext, mapper)
    //     {
    //         _mediator = mediator;
    //     }
    //
    //     public async Task<IssueDetailsDto> Handle(EditIssueCommand request, CancellationToken cancellationToken)
    //     {
    //         // var issues = Context.Set<Entities.Issue>();
    //         // var issue = await issues.FindAsync(request.Id);
    //         //
    //         // var assignee = await Context.Set<TeamMember>().FirstOrDefaultAsync(
    //         //     x => x.User.UserName == request.Assignee,
    //         //     cancellationToken: cancellationToken);
    //         //
    //         // var project = await Context.Set<Entities.Project>()
    //         //     .FirstOrDefaultAsync(x => x.Name == request.Project, cancellationToken: cancellationToken);
    //         //
    //         // issue.Update(request.Title, request.Description, assignee, project, request.Estimation, request.Spent,
    //         //     request.State, request.IssueType, request.Priority);
    //         //
    //         // var issueEntry = issues.Update(issue);
    //         // await Context.SaveChangesAsync(cancellationToken);
    //         // var issueUpdated = issueEntry.Entity;
    //         //
    //         //return Mapper.Map<IssueDetailsDto>(issueUpdated)
    //         return null;
    //     }
    // }
}