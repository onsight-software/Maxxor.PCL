using System;
using Maxxor.PCL.ValueObject.Base;
using Maxxor.PCL.ValueObject.Interfaces;

namespace Maxxor.PCL.ValueObject
{
    public class MxGeolocation : MxValueObject<MxGeolocation>, IMxGeolocation
    {
        public MxGeolocation(IMxGpsCoordinates gpsCoordinates, double accuracy, double altitude, double heading, double speed, DateTimeOffset dateTimeOffset)
        {
            GpsCoordinates = gpsCoordinates;
            Accuracy = accuracy;
            Altitude = altitude;
            Heading = heading;
            Speed = speed;
            DateTimeOffset = dateTimeOffset;
        }

        public IMxGpsCoordinates GpsCoordinates { get; }
        public double Accuracy { get; }
        public double Altitude { get; }
        public double Heading { get; }
        public double Speed { get; }
        public DateTimeOffset DateTimeOffset { get; }
    }
}