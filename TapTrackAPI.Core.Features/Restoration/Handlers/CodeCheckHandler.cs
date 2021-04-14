using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Features.Restoration.DTO;

namespace TapTrackAPI.Core.Features.Restoration.Handlers
{
    public class CodeCheckHandler : RequestHandlerBase, IRequestHandler<CheckCodeQuery>
    {

        public CodeCheckHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public Task<Unit> Handle(CheckCodeQuery request, CancellationToken cancellationToken)
        {
            var dbCode = Context.Set<Entities.RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderBy(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode != null && dbCode.Code == userCode)
            {
                dbCode.CodeIsUsed();
            }
            Context.SaveChanges();
            return default;
        }
    }
}
