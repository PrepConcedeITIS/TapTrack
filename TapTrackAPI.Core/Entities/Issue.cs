using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Enums;

namespace TapTrackAPI.Core.Entities
{
    public class Issue : EntityBase
    {
        protected Issue()
        {
        }

        public Issue(string title, string description, long creatorId, long assigneeId, Guid projectId, IssueType type,
            Priority priority)
        {
            Title = title;
            Description = description;
            CreatorId = creatorId;
            AssigneeId = assigneeId;
            ProjectId = projectId;
            Estimation = TimeSpan.Zero;
            Spent = TimeSpan.Zero;
            State = State.New;
            IssueType = type;
            Priority = priority;
            Created = DateTime.Now;
        }

        public string Title { get; protected set; }
        public string Description { get; protected set; }
        [JsonIgnore]
        public virtual TeamMember Creator { get; protected set; }
        public long CreatorId { get; set; }
        [JsonIgnore]
        public virtual TeamMember Assignee { get; protected set; }
        public long AssigneeId { get; set; }
        [JsonIgnore]
        public virtual Project Project { get; protected set; }
        public Guid ProjectId { get; set; }
        public TimeSpan Estimation { get; protected set; }
        public TimeSpan Spent { get; protected set; }
        public State State { get; protected set; }
        public IssueType IssueType { get; protected set; }
        public Priority Priority { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime LastUpdated { get; protected set; }

        public virtual ICollection<Comment> Comment { get; protected set; }

        public void Update(string title, string description, TeamMember assignee, Project project, TimeSpan estimation,
            TimeSpan spent, State state, IssueType issueType, Priority priority)
        {
            Title = title;
            Description = description;
            AssigneeId = assignee.Id;
            ProjectId = project.Id;
            Estimation = estimation;
            Spent = spent;
            State = state;
            IssueType = issueType;
            Priority = priority;
            LastUpdated = DateTime.Now;
        }

        public void UpdatePriority(Priority priority)
        {
            Priority = priority;
        }

        public void UpdateState(State state)
        {
            State = state;
        }
    }
}