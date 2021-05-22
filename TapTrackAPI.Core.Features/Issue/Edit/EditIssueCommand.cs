using System;
using MediatR;
using TapTrackAPI.Core.Enums;
using TapTrackAPI.Core.Features.Issue.Dtos;

namespace TapTrackAPI.Core.Features.Issue.Edit
{
    public class EditIssueCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }

        public EditIssueCommand()
        {
            
        }
        
        public EditIssueCommand(Guid id, string title, string description, Guid projectId)
        {
            Id = id;
            Title = title;
            Description = description;
            ProjectId = projectId;
        }
    }
}