using Maxxor.PCL.Resources;
using Maxxor.PCL.Result;

namespace Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber
{
    public class MxSouthAfricanIdCardError : MxErrorCondition
    {
        private MxSouthAfricanIdCardError(string value, string description) : base(value, description)
        {
        }

        public static readonly MxSouthAfricanIdCardError InvalidScan = new MxSouthAfricanIdCardError(nameof(InvalidScan),
            MxErrorStrings.IdCard_InvalidScan);
    }
}