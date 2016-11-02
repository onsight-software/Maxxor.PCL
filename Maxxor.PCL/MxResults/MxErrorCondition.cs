using Maxxor.PCL.MxResults.Interfaces;
using Maxxor.PCL.MxStrings;

namespace Maxxor.PCL.MxResults
{
    public class MxErrorCondition : IMxErrorCondition
    {
        public override string ToString()
        {
            return Value;
        }

        protected MxErrorCondition(string value, string description)
        {
            Value = value;
            Description = description;
        }

        public string Value { get; }
        public string Description { get; }


        public static readonly MxErrorCondition Unspecified = new MxErrorCondition("Unspecified", MxErrorStrings.Unspecified);
        public static readonly MxErrorCondition Cancelled = new MxErrorCondition("Cancelled", MxErrorStrings.Cancelled);
        public static readonly MxErrorCondition Crash = new MxErrorCondition("Crash", MxErrorStrings.Crash);


    }
}