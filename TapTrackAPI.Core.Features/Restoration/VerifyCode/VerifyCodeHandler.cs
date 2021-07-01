using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TapTrackAPI.Core.Base.Handlers;

namespace TapTrackAPI.Core.Features.Restoration.VerifyCode
{
    [UsedImplicitly]
    public class VerifyCodeHandler : BaseHandler<VerifyCodeCommand, Unit?>
    {
        public VerifyCodeHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Unit?> Handle(VerifyCodeCommand request, CancellationToken cancellationToken)
        {
            var dbCode = await DbContext.Set<Entities.RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            var userCode = request.UserCode;
            if (dbCode != null && dbCode.Code == userCode && DateTime.Compare(dbCode.ExpirationDate, DateTime.Now) > 0)
            {
                return Unit.Value;
            }

            return null;
        }
    }
}