using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Enums;
using BindingFlags = System.Reflection.BindingFlags;

namespace TapTrackAPI.Core.Features.Issue.Base
{
    public interface IIssueBuilder
    {
        IIssueBuilder Reset();
        IIssueBuilder SetTitle(string title);
        IIssueBuilder SetDescription(string description);
        IIssueBuilder SetProject(Entities.Project project);
        IIssueBuilder SetCreator(TeamMember creator);
        IIssueBuilder SetState(State state);
        IIssueBuilder SetPriority(Priority priority);
        IIssueBuilder SetType(IssueType type);
        IIssueBuilder AddIdVisible(Entities.Project project);
        IIssueBuilder SetCreationDate(DateTime? dateTime = null);

        Entities.Issue GetResult();
    }

    public class IssueBuilder : IIssueBuilder
    {
        private Entities.Issue _issue;
        private readonly Func<Entities.Issue> _issueCreateLambda;
        private readonly DbContext _dbContext;

        public IssueBuilder(DbContext dbContext)
        {
            _dbContext = dbContext;
            _issueCreateLambda = CreateEmptyIssueCreateLambda();
            _issue = _issueCreateLambda();
        }

        public IssueBuilder(Entities.Issue issue, DbContext dbContext) : this(dbContext)
        {
            _issue = issue;
        }

        private static Func<Entities.Issue> CreateEmptyIssueCreateLambda()
        {
            var newExpression =
                Expression.New(typeof(Entities.Issue).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance,
                    null, Array.Empty<Type>(), null)!);
            var lambda = Expression.Lambda(typeof(Func<Entities.Issue>), newExpression);
            var issueCreateCompiledLambda = (Func<Entities.Issue>) lambda.Compile();

            return issueCreateCompiledLambda;
        }

        public Entities.Issue GetResult()
        {
            return _issue;
        }
        
        public IIssueBuilder Reset()
        {
            _issue = _issueCreateLambda();
            return this;
        }

        public IIssueBuilder SetTitle(string title)
        {
            _issue.UpdateTitle(title);
            return this;
        }

        public IIssueBuilder SetDescription(string description)
        {
            _issue.UpdateDescription(description);
            return this;
        }

        public IIssueBuilder SetProject(Entities.Project project)
        {
            _issue.UpdateProject(project.Id);
            return this;
        }

        public IIssueBuilder SetCreator(TeamMember creator)
        {
            _issue.SetCreator(creator);
            return this;
        }

        public IIssueBuilder SetState(State state)
        {
            _issue.UpdateState(state);
            return this;
        }

        public IIssueBuilder SetPriority(Priority priority)
        {
            _issue.UpdatePriority(priority);
            return this;
        }

        public IIssueBuilder SetType(IssueType type)
        {
            _issue.UpdateIssueType(type);
            return this;
        }

        public IIssueBuilder AddIdVisible(Entities.Project project)
        {
            _issue.SetIdVisible(GetIdVisible(project));
            return this;
        }

        public IIssueBuilder SetCreationDate(DateTime? dateTime = null)
        {
            _issue.SetCreationDate(dateTime ?? DateTime.UtcNow);
            return this;
        }

        private string GetIdVisible(Entities.Project project)
        {
            var lastIssueNumbers = _dbContext.Set<Entities.Issue>()
                .Where(x => x.ProjectId == project.Id)
                .Select(x => x.IdVisible)
                .ToList();
            if (!lastIssueNumbers.Any())
            {
                return $@"{project.IdVisible}-1";
            }

            var num = lastIssueNumbers.Max(x => Convert.ToInt32(x.Split('-')[1]));
            return $@"{project.IdVisible}-{num + 1}";
        }
    }
}