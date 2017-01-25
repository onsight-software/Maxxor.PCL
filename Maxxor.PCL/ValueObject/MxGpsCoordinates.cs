using System;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.ValueObject
{
    public class MxGpsCoordinates : MxValueObject<MxGpsCoordinates>
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public MxGpsCoordinates(double latitude, double longitude)
        {
            Latitude = latitude > -90 && latitude < 90 ? latitude : double.NaN;
            Longitude = longitude > -180 && longitude < 180 ? longitude : double.NaN;
        }

        public bool IsValidCoordinates => !double.IsNaN(Latitude) && !double.IsNaN(Longitude);

        public override string ToString()
        {
            return Latitude + "," + Longitude;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MxGpsCoordinates))
                return false;

            const double epsilon = 0.00001;
            var otherLocation = (MxGpsCoordinates)obj;
            if (!IsValidCoordinates && !otherLocation.IsValidCoordinates)
                return true;

            return (Math.Abs(otherLocation.Latitude - Latitude) < epsilon) &&
                   (Math.Abs(otherLocation.Longitude - Longitude) < epsilon);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Latitude.GetHashCode();
                hash = hash * 23 + Longitude.GetHashCode();

                return hash;
            }
        }
    }
}
