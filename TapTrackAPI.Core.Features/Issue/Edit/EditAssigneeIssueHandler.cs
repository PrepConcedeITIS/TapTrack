using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditAssigneeIssueHandler: BaseHandler<EditAssigneeIssueCommand, Guid>
    {
        private readonly INotificationService _notificationService;
        public EditAssigneeIssueHandler(DbContext context, 
            IMapper mapper, 
            INotificationService notificationService) : base(context, mapper)
        {
            _notificationService = notificationService;
        }

        public override async Task<Guid> Handle(EditAssigneeIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = DbContext.Set<Entities.Issue>();
            var issue = await issues.FindAsync(Guid.Parse(request.Id));
            var teamMember = await DbContext.Set<TeamMember>()
                .FirstOrDefaultAsync(x => x.User.UserName == request.Assignee, cancellationToken);
            issue.UpdateAssignee(teamMember?.Id);
            await DbContext.SaveChangesAsync(cancellationToken);
            await _notificationService.SendIssueAssignmentNotification(request.User, issue, teamMember);
            return issue.Id;
        }
    }
}