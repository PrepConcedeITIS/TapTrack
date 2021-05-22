using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
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

        public Issue(string title, string description, Guid projectId, TeamMember creator, string idVisible)
        {
            Title = title;
            Description = description;
            ProjectId = projectId;
            CreatorId = creator.Id;
            State = State.New;
            Priority = Priority.Normal;
            IssueType = IssueType.Task;
            Created = DateTime.Now;
            IdVisible = idVisible;
        }

        public string Title { get; protected set; }

        public string Description { get; protected set; }

        public long CreatorId { get; set; }
        [JsonIgnore] public virtual TeamMember Creator { get; protected set; }

        public long? AssigneeId { get; set; }
        [JsonIgnore] [CanBeNull] public virtual TeamMember Assignee { get; protected set; }

        public Guid ProjectId { get; set; }
        [JsonIgnore] public virtual Project Project { get; protected set; }

        public TimeSpan Estimation { get; protected set; }

        public TimeSpan Spent { get; protected set; }

        public State State { get; protected set; }

        public IssueType IssueType { get; protected set; }

        public Priority Priority { get; protected set; }

        public DateTime Created { get; protected set; }

        public DateTime LastUpdated { get; protected set; }


        public virtual ICollection<Comment> Comment { get; protected set; }

        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void UpdateProject(Guid projectId)
        {
            AssigneeId = null;
            ProjectId = projectId;
        }

        public void UpdatePriority(Priority priority)
        {
            Priority = priority;
        }

        public void UpdateState(State state)
        {
            State = state;
        }
        
        public void UpdateIssueType(IssueType type)
        {
            IssueType = type;
        }
        
        public void UpdateAssignee(long? assigneeId)
        {
            AssigneeId = assigneeId;
        }
    }
}