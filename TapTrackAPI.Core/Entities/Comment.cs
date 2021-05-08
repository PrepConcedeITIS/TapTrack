using System;
using TapTrackAPI.Core.Base;

namespace TapTrackAPI.Core.Entities
{
    public class Comment : EntityBase
    {
        public virtual TeamMember Author { get; protected set; }
        public long AuthorId { get; protected set; }
        public virtual Issue Issue { get; protected set; }
        public Guid? IssueId { get; protected set; }
        public virtual Article Article { get; protected set; }
        public Guid? ArticleId { get; protected set; }
        public string Text { get; protected set; }
        public DateTime Created { get; protected set; }
        public DateTime LastUpdated { get; protected set; }
        public bool IsDeleted { get; protected set; }

        public Comment(long authorId, string entityType, Guid entityId, string text)
        {
            AuthorId = authorId;
            switch (entityType)
            {
                case "Issue":
                    IssueId = entityId;
                    ArticleId = null;
                    break;
                case "Article":
                    ArticleId = entityId;
                    IssueId = null;
                    break;
            }

            Text = text;
            var time = DateTime.Now;
            Created = time;
            LastUpdated = time;
        }

        protected Comment()
        {
        }
    }
}