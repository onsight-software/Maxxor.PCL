using Maxxor.PCL.Resources;
using Maxxor.PCL.Result;

namespace Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber
{
    public class MxSouthAfricanIdNumberError : MxErrorCondition
    {
        private MxSouthAfricanIdNumberError(string value, string description) : base(value, description)
        {
        }

        public static readonly MxSouthAfricanIdNumberError InvalidDateOfBirth = new MxSouthAfricanIdNumberError(nameof(InvalidDateOfBirth),
            MxErrorStrings.IdNumber_Invalid);
        public static readonly MxSouthAfricanIdNumberError InvalidGender = new MxSouthAfricanIdNumberError(nameof(InvalidGender),
            MxErrorStrings.IdNumber_Invalid);
        public static readonly MxSouthAfricanIdNumberError InvalidChecksum = new MxSouthAfricanIdNumberError(nameof(InvalidChecksum),
            MxErrorStrings.IdNumber_Invalid);
    }
}