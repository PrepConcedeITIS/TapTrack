using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TapTrackAPI.Core.Constants;
using TapTrackAPI.Core.Interfaces;

namespace TapTrackAPI.Core.Features.Project.Delete
{
    public class ProjectDeleteAsyncCommandHandler: IRequestHandler<ProjectDeleteCommand>
    {
        private IMailSender _mailSender;
        private IConfiguration _configuration;
        private DbContext _dbContext;
        public ProjectDeleteAsyncCommandHandler(IMailSender mailSender, IConfiguration configuration, DbContext dbContext)
        {
            _mailSender = mailSender;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);

            _dbContext.Set<Entities.Project>().Remove(project);

            var needToSendEmail = Convert.ToBoolean(_configuration[ConfigurationConstants.EmailNotificationsEnabled]);
            if (!needToSendEmail)
            {
                return Unit.Value;
            }

            await SendScopeOfEmails(project.Team.Select(x => x.User.Email));
        }

        public async Task SendScopeOfEmails(IEnumerable<string> emails)
        {
            
        }
    }
}