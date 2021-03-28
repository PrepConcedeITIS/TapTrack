using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;
using TapTrackAPI.Core.Features.Project.Records;

namespace TapTrackAPI.Core.Features.Project.Handlers
{
    public class GetUniquenessOfIdQueryHandler : IAsyncQueryHandler<GetUniquenessOfIdQuery, bool>
    {
        private readonly DbContext _dbContext;

        public GetUniquenessOfIdQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(GetUniquenessOfIdQuery input)
        {
            var result = (await _dbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.IdVisible == input.IdVisible.ToUpperInvariant())) == null;
            return result;
        }
    }
}