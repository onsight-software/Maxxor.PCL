using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxxor.PCL.ValueObject.Interfaces
{
    public interface IMxGpsCoordinates
    {
        double Latitude { get; }
        double Longitude { get; }
        bool IsValidCoordinates { get; }
    }
}
