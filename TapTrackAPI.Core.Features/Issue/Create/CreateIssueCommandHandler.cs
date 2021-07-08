using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Issue.Base;

namespace TapTrackAPI.Core.Features.Issue.Create
{
    [UsedImplicitly]
    public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Guid>
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IIssueBuilder _issueBuilder;

        public CreateIssueCommandHandler(DbContext dbContext,
            UserManager<User> userManager, IIssueBuilder issueBuilder)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _issueBuilder = issueBuilder;
        }

        public Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var creatorId = _userManager.GetUserIdGuid(request.ClaimsPrincipal);

            var project = _dbContext.Set<Entities.Project>()
                .FirstOrDefault(x => x.Id == request.Project);
            var creator = _dbContext.Set<TeamMember>().FirstOrDefault(x => x.UserId == creatorId);

            if (project == null || creator == null)
                return null;

            var issue = _issueBuilder
                .Reset()
                .SetTitle(request.Name)
                .SetDescription(request.Description)
                .SetProject(project)
                .SetCreator(creator)
                .AddIdVisible(project)
                .SetCreationDate()
                .GetResult();
            var entity = _dbContext.Add(issue);
            _dbContext.SaveChanges();
            return Task.FromResult(entity.Entity.Id);
        }
    }
}