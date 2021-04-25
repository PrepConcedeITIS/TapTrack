using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TapTrackAPI.Core.Features.Issue
{
    public class TimeSpanFormatter
    {
        public string FormatterFromTimeSpan(TimeSpan date)
        {
            var weeks = date.Days / 5;
            var days = date.Days % 5;
            var hours = date.Hours;
            var output = weeks + "w " + days + "d " + hours + "h ";
            return output;
        }

        public TimeSpan FormatterFromString(string date)
        {
            var splitter = date.Split('w', 'd', 'h');
            var weeks = int.Parse(splitter[0]);
            var days = int.Parse(splitter[1]);
            var hours = int.Parse(splitter[2]);
            var allDays = weeks * 5 + days;
            return new TimeSpan(allDays, hours, 0, 0);
        }
    }
}
