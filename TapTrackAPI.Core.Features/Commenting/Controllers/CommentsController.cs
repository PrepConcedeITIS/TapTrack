using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.Commenting.Commands;
using TapTrackAPI.Core.Features.Commenting.Queries;

namespace TapTrackAPI.Core.Features.Commenting.Controllers
{
    public class CommentsController : AuthorizedApiController
    {
        protected UserManager<User> UserManager { get; }

        public CommentsController(IMediator mediator, UserManager<User> userManager) : base(mediator)
        {
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntityComments([FromQuery] string entityType, [FromQuery] Guid entityId)
        {
            var result = await Mediator.Send(new GetAllEntityCommentsQuery(entityId, entityType));
            if (result == null)
                return BadRequest("Entity type is wrong");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }
    }
}