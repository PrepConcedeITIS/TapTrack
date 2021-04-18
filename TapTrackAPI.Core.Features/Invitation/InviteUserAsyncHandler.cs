using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class InviteUserAsyncHandler : IRequestHandler<InviteUserCommand, InvitatonDto>
    {
        public Task<InvitatonDto> Handle(InviteUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}