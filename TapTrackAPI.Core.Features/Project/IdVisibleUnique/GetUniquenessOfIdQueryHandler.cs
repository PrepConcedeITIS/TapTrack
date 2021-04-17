using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TapTrackAPI.Core.Features.Project.IdVisibleUnique
{
    [UsedImplicitly]
    public class GetUniquenessOfIdQueryHandler : IRequestHandler<GetUniquenessOfIdQuery, bool>
    {
        private readonly DbContext _dbContext;

        public GetUniquenessOfIdQueryHandler(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(GetUniquenessOfIdQuery input, CancellationToken cancellationToken)
        {
            var result = (await _dbContext.Set<Entities.Project>()
                .FirstOrDefaultAsync(x => x.IdVisible == input.IdVisible.ToUpperInvariant(),
                    cancellationToken: cancellationToken)) == null;
            return result;
        }
    }
}