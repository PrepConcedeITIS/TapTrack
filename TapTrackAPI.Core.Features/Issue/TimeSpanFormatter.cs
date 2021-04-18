using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Features.Issue
{
    public class TimeSpanFormatter
    {
        public string Formatter(TimeSpan date)
        {
            var weeks = date.Days / 5;
            var days = date.Days % 5;
            var hours = date.Hours;
            var output = weeks + "w " + days + "d " + hours + "h ";
            return output;
        }
    }
}
