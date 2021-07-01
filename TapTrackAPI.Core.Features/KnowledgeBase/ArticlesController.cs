using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Extensions;
using TapTrackAPI.Core.Features.KnowledgeBase.ById;
using TapTrackAPI.Core.Features.KnowledgeBase.Create;
using TapTrackAPI.Core.Features.KnowledgeBase.Delete;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.GetProjectOptions;
using TapTrackAPI.Core.Features.KnowledgeBase.List;
using TapTrackAPI.Core.Features.KnowledgeBase.Update;
using TapTrackAPI.Core.Records;

namespace TapTrackAPI.Core.Features.KnowledgeBase
{
    public class ArticlesController : AuthorizedApiController
    {
        protected UserManager<User> UserManager { get; }

        public ArticlesController(IMediator mediator, UserManager<User> userManager) : base(mediator)
        {
            UserManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectWithArticlesDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllArticlesQuery(UserManager.GetUserIdGuid(User))));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FullArticleDto>> GetById([FromRoute] Guid id)
        {
            var article = await Mediator.Send(new GetArticleByIdQuery(id));
            if (article != null)
                return Ok(article);
            return NotFound();
        }

        [HttpGet("options")]
        public async Task<ActionResult<List<OptionDto>>> GetAllOptions()
        {
            return Ok(await Mediator.Send(new GetProjectOptionsQuery(User)));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateArticleCommand command)
        {
            return Ok(await Mediator.Send(command with {AppUser = User}));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateArticleCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteArticleCommand command)
        {
            return Ok(await Mediator.Send(command with {UserId = UserManager.GetUserIdGuid(User)}));
        }
    }
}