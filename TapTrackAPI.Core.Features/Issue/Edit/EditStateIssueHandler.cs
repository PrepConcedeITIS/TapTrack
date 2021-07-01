using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditStateIssueHandler : BaseHandler<EditStateIssueCommand, Guid>
    {
        private readonly INotificationService _notificationService;

        public EditStateIssueHandler(DbContext dbContext, IMapper mapper, INotificationService notificationService) : base(dbContext, mapper)
        {
            _notificationService = notificationService;
        }

        public override async Task<Guid> Handle(EditStateIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = DbContext.Set<Entities.Issue>().Include(x => x.Assignee);
            var (id, state, claimsPrincipal) = request;
            var issue = await issues.Include(x=>x.Assignee)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id), cancellationToken: cancellationToken);
            var previousStatus = issue.State;
            issue.UpdateState(state);
            await DbContext.SaveChangesAsync(cancellationToken);
            //todo: return to front or log
            var notificationResult = await _notificationService.SendIssueStatusChangeNotification(claimsPrincipal, previousStatus, issue);
            return issue.Id;
        }
    }
}
