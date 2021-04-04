using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Base
{
    public class RequestHandlerBase
    {
        protected DbContext Context { get; }
        protected IMapper Mapper { get; }

        public RequestHandlerBase(DbContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
        }
    }
}