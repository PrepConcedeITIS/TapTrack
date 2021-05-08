using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Features.Commenting.Base
{
    public class BaseCommandHandler
    {
        public DbContext DbContext { get; }
        public IMapper Mapper { get; }

        public BaseCommandHandler(DbContext dbContext, IMapper mapper)
        {
            DbContext = dbContext;
            Mapper = mapper;
        }
    }
}