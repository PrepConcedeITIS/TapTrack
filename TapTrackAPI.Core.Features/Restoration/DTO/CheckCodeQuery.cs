using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Features.Restoration.DTO
{
    public class CheckCodeQuery : IRequest
    {
        public string UserMail { get; set; }
        public int UserCode { get; set; }
    }
}
