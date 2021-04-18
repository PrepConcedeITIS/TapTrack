using System;
using MediatR;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Features.Issue.Queries
{
    public class EditIssueCommand : IRequest<Entities.Issue>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Assignee { get; set; }
        public string Project { get; set; }
        public TimeSpan Estimation { get; set; }
        public TimeSpan Spent { get; set; }
        public State State { get; set; }
        public IssueType IssueType { get; set; }
        public Priority Priority { get; set; }

        public EditIssueCommand(Guid id, string title, string description, string assignee, string project,
            TimeSpan estimation,
            TimeSpan spent, State state, IssueType issueType, Priority priority)
        {
            Id = id;
            Title = title;
            Description = description;
            Assignee = assignee;
            Project = project;
            Estimation = estimation;
            Spent = spent;
            State = state;
            IssueType = issueType;
            Priority = priority;
        }

        public EditIssueCommand()
        {
        }
    }
}