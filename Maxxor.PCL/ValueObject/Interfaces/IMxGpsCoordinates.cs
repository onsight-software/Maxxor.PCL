
namespace Maxxor.PCL.ValueObject.Interfaces
{
    public interface IMxGpsCoordinates
    {
        double Latitude { get; }
        double Longitude { get; }
        bool IsValidCoordinates { get; }
    }
}
