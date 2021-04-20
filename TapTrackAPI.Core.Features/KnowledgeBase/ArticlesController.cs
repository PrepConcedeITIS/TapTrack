using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.KnowledgeBase.Commands;
using TapTrackAPI.Core.Features.KnowledgeBase.DTOs;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase
{
    public class ArticlesController : AuthorizedApiController
    {
        public ArticlesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectWithArticlesDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllArticlesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FullArticleDto>> GetById(Guid id)
        {
            var article = await Mediator.Send(new GetArticleByIdQuery(id));
            if (article != null)
                return Ok(article);
            return NotFound();
        }

        [HttpGet("options")]
        public async Task<ActionResult<List<OptionDto>>> GetAllOptions()
        {
            return Ok(await Mediator.Send(new GetAllOptionsQuery(User)));
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateArticleCommand command)
        {
            return Ok(await Mediator.Send(command with {AppUser = User}));
        }
    }
}