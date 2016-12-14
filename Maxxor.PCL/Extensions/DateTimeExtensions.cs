using System;

namespace Maxxor.PCL.Extensions
{
    public static class DateTimeExtensions
    {
        public static long ToUnixTimestamp(this DateTime dateTimeToConvert)
        {
            return (long)dateTimeToConvert.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}