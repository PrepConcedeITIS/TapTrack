using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.KnowledgeBase.Dtos;
using TapTrackAPI.Core.Features.KnowledgeBase.Queries;

namespace TapTrackAPI.Core.Features.KnowledgeBase
{
    public class ArticleController : AuthorizedApiController
    {
        public ArticleController(IMediator mediator) : base(mediator)
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
            var article = await Mediator.Send(new GetArticleByIdQuery {Id = id});
            if (article != null)
                return Ok(article);
            return NotFound();
        }
    }
}