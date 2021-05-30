using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.TelegramBot.Interfaces;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    [UsedImplicitly]
    public class EditStateIssueHandler : RequestHandlerBase, IRequestHandler<EditStateIssueCommand, Guid>
    {
        private readonly INotificationService _notificationService;

        public EditStateIssueHandler(DbContext dbContext, IMapper mapper, INotificationService notificationService) : base(dbContext, mapper)
        {
            _notificationService = notificationService;
        }

        public async Task<Guid> Handle(EditStateIssueCommand request, CancellationToken cancellationToken)
        {
            var issues = Context.Set<Entities.Issue>().Include(x => x.Assignee);
            var issue = await issues.Include(x=>x.Assignee)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id), cancellationToken: cancellationToken);
            var previousStatus = issue.State;
            issue.UpdateState(request.State);
            await Context.SaveChangesAsync(cancellationToken);
            var notificationResult = await _notificationService.SendIssueStatusChangeNotification(request.User, previousStatus, issue);
            Console.WriteLine(notificationResult);
            return issue.Id;
        }
    }
}
