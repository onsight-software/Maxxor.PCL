using System;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.ValueObject
{
    public class MxDate : MxValueObject<MxDate>
    {
        public MxDate(int day, int month, int year)
        {
            if (day > 31)
            {
                throw new ArgumentException("Day cannot be greater than 31");
            }
            if (month > 12)
            {
                throw new ArgumentException("Month cannot be greater than 12");
            }

            Day = day;
            Month = month;
            Year = year;
        }

        public int Day { get; }
        public int Month { get; }
        public int Year { get; }

        public override string ToString()
        {
            return Day + "-" + Month + "-" + Year;
        }
    }
}