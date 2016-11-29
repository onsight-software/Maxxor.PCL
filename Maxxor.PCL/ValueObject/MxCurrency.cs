using System.Reflection;
using System.Resources;
using Maxxor.PCL.ValueObject.Base;
using Maxxor.PCL.ValueObject.Interfaces;

namespace Maxxor.PCL.ValueObject
{
    public class MxCurrency :  MxValueObject<MxCurrency>, IMxCurrency
    {
        public MxCurrency(string currencyCode)
        {
            CurrencyCode = currencyCode;
            SetCurrencySymbol();
        }

        private void SetCurrencySymbol()
        {
            var rm = new ResourceManager("Maxxor.PCL.Resources.MxCurrencySymbol", typeof(MxCurrency).GetTypeInfo().Assembly);
            CurrencySymbol = rm.GetString(CurrencyCode) ?? string.Empty;
        }

        public string CurrencyCode { get; }
        public string CurrencySymbol { get; private set; } = string.Empty;
    }
}