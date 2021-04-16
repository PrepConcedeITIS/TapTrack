using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using TapTrackAPI.Core.Base;
using TapTrackAPI.Core.Entities;
using TapTrackAPI.Core.Features.Restoration.DTO;

namespace TapTrackAPI.Core.Features.Restoration.Handlers
{
    public class CodeCheckHandler : RequestHandlerBase, IRequestHandler<CheckCodeQuery, RestorationCode>
    {
        public CodeCheckHandler(DbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task<RestorationCode> Handle(CheckCodeQuery request, CancellationToken cancellationToken)
        {
            var dbCode = Context.Set<Entities.RestorationCode>()
                .Where(x => x.Email == request.UserMail)
                .OrderByDescending(x => x.CreationDate)
                .FirstOrDefault();
            var userCode = request.UserCode;
            if (dbCode != null && dbCode.Code == userCode && DateTime.Compare(dbCode.ExpirationDate,DateTime.Now) > 0)
            {
                return dbCode;
            }

            return default;
        }
    }
}
