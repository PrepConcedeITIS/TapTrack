using System;
using System.Collections.Generic;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class Article : EntityBase
    {
        public virtual Project BelongsTo { get; protected set; }
        public Guid BelongsToId { get; protected set; }
        public string Title { get; protected set; }
        public virtual TeamMember CreatedBy { get; protected set; }
        public long CreatedById { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public virtual TeamMember UpdatedBy { get; protected set; }
        public long UpdatedById { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string Content { get; protected set; }
        public virtual ICollection<Comment> Comments { get; protected set; }

        public Article(Guid belongsToId, string title, long createdById, DateTime createdAt, long updatedById,
            DateTime updatedAt, string content)
        {
            BelongsToId = belongsToId;
            Title = title;
            CreatedById = createdById;
            CreatedAt = createdAt;
            UpdatedById = updatedById;
            UpdatedAt = updatedAt;
            Content = content;
        }

        protected Article()
        {
        }

        public void Update(string title, long teamMemberId, string content)
        {
            Title = title;
            UpdatedById = teamMemberId;
            UpdatedAt = DateTime.UtcNow;
            Content = content;
        }
    }
}