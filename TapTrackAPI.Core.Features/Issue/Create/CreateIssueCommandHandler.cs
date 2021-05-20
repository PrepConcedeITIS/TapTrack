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

namespace TapTrackAPI.Core.Features.Issue.Create
{
    [UsedImplicitly]
    public class CreateIssueCommandHandler: IRequestHandler<CreateIssueCommand, Guid>
    {
        private readonly DbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public CreateIssueCommandHandler(DbContext dbContext, 
            UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public Task<Guid> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
        {
            var creatorId = _userManager.GetUserIdGuid(request.ClaimsPrincipal);
            
            var project = _dbContext.Set<Entities.Project>()
                .FirstOrDefault(x => x.Id == request.Project);
            var creator = _dbContext.Set<TeamMember>().FirstOrDefault(x => x.UserId == creatorId);
            
            if (project == null || creator == null) 
                return null;
            
            var idVisible = GetIdVisible(request.Project, project.IdVisible);

            var issue = new Entities.Issue(request.Name, request.Description, request.Project, creator, idVisible);
            var entity = _dbContext.Add(issue);
            _dbContext.SaveChanges();
            return Task.FromResult(entity.Entity.Id);
        }

        private string GetIdVisible(Guid projectId, string idVisible)
        {
            var lastIssueNumbers = _dbContext.Set<Entities.Issue>()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.IdVisible)
                .ToList();
            if (!lastIssueNumbers.Any())
            {
                return $@"{idVisible}-1";
            }
            
            var num = lastIssueNumbers.Max(x => Convert.ToInt32(x.Split('-')[1]));
            return $@"{idVisible}-{num+1}";
        }
    }
}