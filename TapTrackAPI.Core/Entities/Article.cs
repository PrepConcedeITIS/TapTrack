using System;
using System.Collections.Generic;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class Article : EntityBase
    {
        public string Title { get; protected set; }
        public virtual TeamMember CreatedBy { get; protected set; }
        public long CreatedById { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public virtual TeamMember UpdatedBy { get; protected set; }
        public long UpdatedById { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public string Content { get; protected set; }
        
        public virtual ICollection<Comment> Comment { get; protected set; }

        public Article(string title, long createdById, DateTime createdAt, long updatedById, DateTime updatedAt,
            string content)
        {
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
    }
}