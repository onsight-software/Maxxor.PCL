
using System;

namespace Maxxor.PCL.ValueObject.Interfaces
{
    public interface IMxGeolocation
    {
        IMxGpsCoordinates GpsCoordinates { get; }
        double Accuracy { get; }
        double Altitude { get; }
        double Heading { get; }
        double Speed { get; }
        DateTimeOffset DateTimeOffset { get;  }
    }
}
