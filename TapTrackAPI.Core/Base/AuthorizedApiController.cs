using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace TapTrackAPI.Core.Base
{
     //[Authorize(AuthenticationSchemes = "Bearer")]
    public class AuthorizedApiController : ApiBaseController
    {
        public AuthorizedApiController(IMediator mediator) : base(mediator)
        {
        }
    }
}