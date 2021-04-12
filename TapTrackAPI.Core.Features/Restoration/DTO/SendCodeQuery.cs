using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TapTrackAPI.Core.Entities;

namespace TapTrackAPI.Core.Features.Restoration.DTO
{
    public class SendCodeQuery : IRequest
    {
        public string UserMail { get; set; }
    }
}
