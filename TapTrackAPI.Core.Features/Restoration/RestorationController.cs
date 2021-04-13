using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Restoration.DTO;

namespace TapTrackAPI.Core.Features.Restoration
{
    public class RestorationController : ApiBaseController
    {
        public RestorationController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        
        public async Task<IActionResult> SendCode([FromBody] SendCodeQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("CheckCode")]

        public async Task<IActionResult> CheckCode([FromBody] CheckCodeQuery query)
        {
            var a = await Mediator.Send(query);
            return Ok(a);
        }

    }
}
