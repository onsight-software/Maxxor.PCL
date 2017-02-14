
using System;

namespace Maxxor.PCL.ValueObject.Interfaces
{
    public interface IMxGeolocation
    {
        IMxGpsCoordinates GpsCoordinates { get; }
        double Accuracy { get; }
        DateTimeOffset DateTimeOffset { get;  }
    }
}
