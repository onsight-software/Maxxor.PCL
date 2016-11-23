namespace Maxxor.PCL.Tests.TestObjects
{
    public class ExpandedAddress : Address
    {
        private readonly string _address2;

        public ExpandedAddress(string address1, string address2, string city, string state)
            : base(address1, city, state)
        {
            _address2 = address2;
        }

        public string Address2
        {
            get { return _address2; }
        }

    }
}