using System;
using Maxxor.PCL.ValueObject.Base;
using Maxxor.PCL.ValueObject.Interfaces;

namespace Maxxor.PCL.ValueObject
{
    public class MxGeolocation : MxValueObject<MxGeolocation>, IMxGeolocation
    {
        public MxGeolocation(IMxGpsCoordinates gpsCoordinates, double accuracy, DateTimeOffset dateTimeOffset)
        {
            GpsCoordinates = gpsCoordinates;
            Accuracy = accuracy;
            DateTimeOffset = dateTimeOffset;
        }

        public IMxGpsCoordinates GpsCoordinates { get; }
        public double Accuracy { get; }
        public DateTimeOffset DateTimeOffset { get; }
    }
}