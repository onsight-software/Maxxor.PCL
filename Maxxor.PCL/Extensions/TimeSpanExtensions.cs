using System;
using Maxxor.PCL.Resources;

namespace Maxxor.PCL.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToIntervalString(this TimeSpan timespan)
        {
            if (timespan.Days > 365)
                return (int)(timespan.Days / 365.2425) + MxTimeStrings.Timespan_abbreviation_years;
            if (timespan.Days > 30)
                return (int)(timespan.Days / 30.436875) + MxTimeStrings.Timespan_abbreviation_months;
            if (timespan.Days >= 1)
                return timespan.Days + MxTimeStrings.Timespan_abbreviation_days;
            if (timespan.Hours >= 1)
                return timespan.Hours + MxTimeStrings.Timespan_abbreviation_hours;
            if (timespan.Minutes >= 1)
                return timespan.Minutes + MxTimeStrings.Timespan_abbreviation_minutes;
            return timespan.Seconds + MxTimeStrings.Timespan_abbreviation_seconds;
        }
    }
}