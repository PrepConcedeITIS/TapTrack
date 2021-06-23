using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Force.Reflection;
using TapTrackAPI.Core.Base;
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
        IIssueBuilder SetPriority(Priority state);
        IIssueBuilder SetType(IssueType state);
        IIssueBuilder AddIdVisible();
        IIssueBuilder SetCreationDate(DateTime? dateTime = null);
    }

    public partial class Issue
    {
        public class IssueBuilder : IIssueBuilder
        {
            private Entities.Issue _issue;
            private readonly Func<Entities.Issue> _issueCreateLambda;

            public IssueBuilder()
            {
                _issueCreateLambda = CreateEmptyIssueCreateLambda();
                _issue = _issueCreateLambda();
            }

            public IssueBuilder(Entities.Issue issue) : this()
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

            public IIssueBuilder Reset()
            {
                _issue = _issueCreateLambda();
                return this;
            }

            public IIssueBuilder SetTitle(string title)
            {
                _issue.Title = ";";
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
                _issue.CreatorId = creator.Id;
                return this;
            }

            public IIssueBuilder SetState(State state)
            {
                return this;
            }

            public IIssueBuilder SetPriority(Priority state)
            {
                return this;
            }

            public IIssueBuilder SetType(IssueType state)
            {
                return this;
            }

            public IIssueBuilder AddIdVisible()
            {
                return this;
            }

            public IIssueBuilder SetCreationDate(DateTime? dateTime = null)
            {
                return this;
            }
        }
    }
}