using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using Maxxor.PCL.ValueObject.Base;
using Maxxor.PCL.ValueObject.Interfaces;

namespace Maxxor.PCL.ValueObject
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
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

        public static MxCurrency ZAR = new MxCurrency(nameof(ZAR));
        public static MxCurrency AUD = new MxCurrency(nameof(AUD));
        public static MxCurrency GBP = new MxCurrency(nameof(GBP));
        public static MxCurrency USD = new MxCurrency(nameof(USD));
        public static MxCurrency CAD = new MxCurrency(nameof(CAD));
    }
}