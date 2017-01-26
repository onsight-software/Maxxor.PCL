using System;
using System.Globalization;
using Maxxor.PCL.ValueObject.Base;
using Maxxor.PCL.ValueObject.Interfaces;

namespace Maxxor.PCL.ValueObject
{
    public class MxGpsCoordinates : MxValueObject<MxGpsCoordinates>, IMxGpsCoordinates
    {
        public double Latitude { get; }
        public double Longitude { get; }

        public MxGpsCoordinates(double latitude, double longitude)
        {
            Latitude = latitude >= -90 && latitude <= 90 ? latitude : double.NaN;
            Longitude = longitude >= -180 && longitude <= 180 ? longitude : double.NaN;
        }

        public bool IsValidCoordinates => !double.IsNaN(Latitude) && !double.IsNaN(Longitude);
        /// <summary>
        /// Always uses a point decimal seperator
        /// </summary>
        /// <returns>string in the point-decimal format of latitude,longitude</returns>
        public override string ToString()
        {
            return IsValidCoordinates ? Latitude.ToString(CultureInfo.InvariantCulture) + "," + Longitude.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }
        /// <summary>
        /// Parses a string in the decimal-point format lat,long
        /// </summary>
        /// <param name="toParse"></param>
        /// <returns>MxGpsCoordinates if valid string is given else null</returns>
        public static MxGpsCoordinates TryParse(string toParse)
        {
            var arr = toParse?.Split(',');
            if (arr?.Length != 2)
                return null;
            
            double latitude = double.NaN;
            double longitude = double.NaN;
            var success = double.TryParse(arr[0], NumberStyles.Number, CultureInfo.InvariantCulture, out latitude);
            success = success && double.TryParse(arr[1], NumberStyles.Number, CultureInfo.InvariantCulture, out longitude);
            return success ? new MxGpsCoordinates(latitude, longitude) : null;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MxGpsCoordinates))
                return false;

            const double epsilon = 0.000001; //Accuracy to roughly 10cm
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
