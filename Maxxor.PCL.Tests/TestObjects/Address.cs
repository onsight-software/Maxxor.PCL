using Maxxor.PCL.ValueObject;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.Tests.TestObjects
{
    public class Address : MxValueObject<Address>
    {
        private readonly string _address1;
        private readonly string _city;
        private readonly string _state;

        public Address(string address1, string city, string state)
        {
            _address1 = address1;
            _city = city;
            _state = state;
        }

        public string Address1
        {
            get { return _address1; }
        }

        public string City
        {
            get { return _city; }
        }

        public string State
        {
            get { return _state; }
        }
    }
}