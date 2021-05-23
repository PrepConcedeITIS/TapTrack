using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Delete
{
    [UsedImplicitly]
    public class ProjectDeleteAsyncCommandHandler : IRequestHandler<ProjectDeleteCommand>
    {
        private readonly IMailSender _mailSender;
        private readonly IConfiguration _configuration;
        private readonly DbContext _dbContext;

        public ProjectDeleteAsyncCommandHandler(IMailSender mailSender, IConfiguration configuration,
            DbContext dbContext)
        {
            _mailSender = mailSender;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var projectWithTeam = await _dbContext.Set<Entities.Project>()
                .Select(p => new
                {
                    Team = p.Team.Select(x => x.User.Email),
                    Project = p
                })
                .FirstOrDefaultAsync(x => x.Project.Id == request.ProjectId, cancellationToken: cancellationToken);

            _dbContext.Set<Entities.Project>().Remove(projectWithTeam.Project);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var needToSendEmail = Convert.ToBoolean(_configuration[ConfigurationConstants.EmailNotificationsEnabled]);
            if (needToSendEmail)
            {
                await SendScopeOfEmails(projectWithTeam.Team, projectWithTeam.Project.Name);
            }

            return Unit.Value;
        }

        private async Task SendScopeOfEmails(IEnumerable<string> emails, string projectName)
        {
            var tasks = new List<Task>();
            foreach (var email in emails)
            {
                var message = new MailMessage(new MailAddress("taptrack@noreply.com", "TapTrack"),
                    new MailAddress(email))
                {
                    Body = $"{projectName} has been deleted by creator",
                    Subject = "Project has been deleted"
                };

                tasks.Add(_mailSender.SendMessageAsync(message));
            }

            await Task.WhenAll(tasks);
        }
    }
}