using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TapTrackAPI.Core.Features.Invitation
{
    public class GetInvitedUserAsyncHandler : IRequestHandler<GetInvitedUserQuery, InvitatonDto>
    {
        public Task<InvitatonDto> Handle(GetInvitedUserQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}