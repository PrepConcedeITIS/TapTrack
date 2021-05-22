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
        private UserManager<User> UserManager { get; }

        public CommentsController(IMediator mediator, UserManager<User> userManager) : base(mediator)
        {
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntityComments([FromQuery] string entityType, [FromQuery] Guid projectId,
            [FromQuery] Guid entityId)
        {
            var result =
                await Mediator.Send(
                    new GetAllEntityCommentsQuery(entityType, projectId, entityId, UserManager.GetUserIdGuid(User)));
            if (result == null)
                return BadRequest("Entity type is wrong");
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }

        [HttpPost, Route("restore")]
        public async Task<IActionResult> RestoreComment([FromBody] RestoreCommentCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCommand([FromBody] DeleteCommentCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }
    }
}