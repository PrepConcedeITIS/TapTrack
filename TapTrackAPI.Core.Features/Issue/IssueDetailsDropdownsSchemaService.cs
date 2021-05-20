using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue
{
    public class IssueDetailsDropdownsSchemaService
    {
        private readonly DbContext _dbContext;

        public IssueDetailsDropdownsSchemaService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IssueDetailsDropdownsSchema GetSchema(Guid projectId)
        {
            var issueTypes = Enum.GetNames(typeof(IssueType));
            var priorities = Enum.GetNames(typeof(Priority));
            var states = Enum.GetNames(typeof(State));
            var assignees = _dbContext.Set<TeamMember>()
                .Where(x => x.ProjectId == projectId)
                .Select(x => x.User.UserName)
                .ToArray();
            return new IssueDetailsDropdownsSchema(issueTypes, priorities, assignees, states);
        }
    }
}