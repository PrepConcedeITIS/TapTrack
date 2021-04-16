using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
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
            var a = await Mediator.Send(query);
            return Ok(a);
        }

        [HttpPost("CheckCode")]

        public async Task<IActionResult> CheckCode([FromBody] CheckCodeQuery query)
        {
            var a = await Mediator.Send(query);
            if (a != null)
            {
                return Ok(a);
            }

            return BadRequest();
        }

        [HttpPost("Password")]

        public async Task<IActionResult> SendNewPassword([FromBody] NewPasswordQuery query)
        {
            var a = await Mediator.Send(query);
            if (a != null)
            {
                return Ok(a);
            }

            return BadRequest();
        }

    }
}
