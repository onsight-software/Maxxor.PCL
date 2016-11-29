using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxCurrencyTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ConstructorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_CurrencySymbol_when_CurrencyCode_is_AUD()
        {
            //Arrange
            var currencyCode = "AUD";

            //Act
            var result = new MxCurrency(currencyCode);

            //Assert
            Assert.That(result.CurrencySymbol, Is.EqualTo("$"));
        }

        [Test]
        public void SHOULD_set_CurrencySymbol_when_CurrencyCode_is_AED()
        {
            //Arrange
            var currencyCode = "AED";

            //Act
            var result = new MxCurrency(currencyCode);

            //Assert
            Assert.That(result.CurrencySymbol, Is.EqualTo("د.إ"));
        }

        [Test]
        public void IF_currency_code_is_invalid_SHOULD_set_CurrencySymbol_to_blank()
        {
            //Arrange
            var currencyCode = "Not gonna work";

            //Act
            var result = new MxCurrency(currencyCode);

            //Assert
            Assert.That(result.CurrencySymbol, Is.EqualTo(""));
        }
    }
}