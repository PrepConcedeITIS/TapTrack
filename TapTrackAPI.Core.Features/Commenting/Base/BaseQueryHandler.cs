using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Features.Commenting.Base
{
    public class BaseQueryHandler
    {
        protected DbContext DbContext { get; }
        protected IMapper Mapper { get; }

        public BaseQueryHandler(DbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}