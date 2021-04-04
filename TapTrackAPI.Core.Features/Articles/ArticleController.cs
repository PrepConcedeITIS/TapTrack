using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Articles
{
    public class ArticleController : AuthorizedApiController
    {
        private DbContext Context { get; }

        public ArticleController(DbContext context, IMediator mediator)
            : base(mediator)
        {
            Context = context;
        }

        [HttpGet("{id}")]
        public Article GetById(Guid id)
        {
            return Context.Set<Article>().Find(id);
        }
    }
}
