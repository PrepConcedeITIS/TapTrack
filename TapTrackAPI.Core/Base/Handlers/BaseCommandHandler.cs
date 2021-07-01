using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Base.Handlers
{
    public abstract class BaseHandler<TInput, TResult>: IRequestHandler<TInput, TResult> 
        where TInput : IRequest<TResult>
    {
        protected DbContext DbContext { get; }
        protected IMapper Mapper { get; }

        protected BaseHandler(DbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }

        public abstract Task<TResult> Handle(TInput request, CancellationToken cancellationToken);
    }
}