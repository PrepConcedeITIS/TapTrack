using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Force.Cqrs;
using MediatR;

namespace TapTrackAPI.Core.Features.Issue.Create
{
    public class CreateIssueCommand: IRequest<Guid>, ICommand<Task<Guid>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Project { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public CreateIssueCommand(string name, string description, Guid projectId, ClaimsPrincipal claimsPrincipal)
        {
            Name = name;
            Description = description;
            Project = projectId;
            ClaimsPrincipal = claimsPrincipal;
        }

        public CreateIssueCommand()
        {
            
        }
    }
}