using Maxxor.PCL.Result;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber
{
    public class MxSouthAfricanIdCard : MxValueObject<MxSouthAfricanIdCard>
    {

        public MxSouthAfricanIdCard()
        {
            IdNumber = new MxSouthAfricanIdNumber();
        }

        public MxSouthAfricanIdNumber IdNumber { get; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public MxResult<MxSouthAfricanIdCard> Populate(string scannedString)
        {
            var strings = scannedString.Split('|');

            var idNumberResult = IdNumber.Populate(strings[4]);
            if (idNumberResult.IsFailure)
            {
                return MxResult.Fail<MxSouthAfricanIdCard>(this, idNumberResult);
            }

            LastName = strings[0];
            FirstName = strings[1];

            return MxResult.Ok(this);
        }
    }
}