using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TapTrackAPI.Core.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        protected IMediator Mediator { get; }

        public ApiBaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}