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

        public CodeCheckHandler(DbContext dbContext, IMapper _mapper) : base(dbContext, _mapper)
        {
        }

        public Task<Unit> Handle(CheckCodeQuery request, CancellationToken cancellationToken)
        {
            var dbCode = Context.Set<Entities.RestorationCode>()
                .Where(x => x.Email == request.UserEmail)
                .OrderBy(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode.Code == userCode)
            {
                dbCode.CodeIsUsed();
            }
            Context.SaveChanges();
            return default;
        }
    }
}
