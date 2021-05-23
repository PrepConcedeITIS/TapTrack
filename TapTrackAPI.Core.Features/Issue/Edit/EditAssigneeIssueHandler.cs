using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditAssigneeIssueHandler: RequestHandlerBase, IRequestHandler<EditAssigneeIssueCommand, Guid>
    {
        private readonly INotificationService _notificationService;
        public EditAssigneeIssueHandler(DbContext context, 
            IMapper mapper, 
            INotificationService notificationService) : base(context, mapper)
        {
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(EditAssigneeIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            var teamMember = await Context.Set<TeamMember>()
                .FirstOrDefaultAsync(x => x.User.UserName == request.Assignee, cancellationToken);
            issue.UpdateAssignee(teamMember?.Id);
            await Context.SaveChangesAsync(cancellationToken);
            await _notificationService.SendIssueAssignmentNotification(request.User, issue, teamMember);
            return issue.Id;
        }
    }
}