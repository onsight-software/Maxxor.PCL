using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxSouthAfricanIdCardTests
{
    [TestFixture]
    public class PopulateTests : BaseUnitTest
    {
        [TestFixture]
        public class IdNumberTests : PopulateTests
        {
            [Test]
            public void SHOULD_extract_Fifth_property_as_IdNumber()
            {
                //Arrange
                var scannedString 
                    = "GWEBANI|LOYISO|F|RSA|8912221087081|22 DEC 1989|RSA|CITIZEN|12 AUG 2016|19668|102709404|" +
                      "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678";

                //Act 
                var result = new MxSouthAfricanIdCard().Populate(scannedString);

                //Assert
                Assert.That(result.Value.IdNumber.IdNumberString, Is.EqualTo("8912221087081"));
            }

            [Test]
            public void WHEN_Fifth_property_is_invalid_id_number_SHOULD_fail()
            {
                //Arrange
                var scannedString
                    = "GWEBANI|LOYISO|F|RSA|89122210s7081|22 DEC 1989|RSA|CITIZEN|12 AUG 2016|19668|102709404" +
                      "|123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678";

                //Act 
                var result = new MxSouthAfricanIdCard().Populate(scannedString);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxSouthAfricanIdNumberError.InvalidChecksum));
            }
        }

        [TestFixture]
        public class NameTests : PopulateTests
        {
            [Test]
            public void SHOULD_extract_First_property_as_LastName()
            {
                //Arrange
                var scannedString
                    = "GWEBANI|LOYISO|F|RSA|8912221087081|22 DEC 1989|RSA|CITIZEN|12 AUG 2016|19668|102709404|" +
                      "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678";

                //Act 
                var result = new MxSouthAfricanIdCard().Populate(scannedString);

                //Assert
                Assert.That(result.Value.LastName, Is.EqualTo("GWEBANI"));
            }

            [Test]
            public void SHOULD_extract_Second_property_as_LastName()
            {
                //Arrange
                var scannedString
                    = "GWEBANI|LOYISO|F|RSA|8912221087081|22 DEC 1989|RSA|CITIZEN|12 AUG 2016|19668|102709404|" +
                      "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678";

                //Act 
                var result = new MxSouthAfricanIdCard().Populate(scannedString);

                //Assert
                Assert.That(result.Value.FirstName, Is.EqualTo("LOYISO"));
            }
        }

        [TestFixture]
        public class InvalidDataTests : PopulateTests
        {

            [Test]
            public void WHEN_data_is_invalid_SHOULD_fail()
            {
                //Arrange
                var scannedString
                    = "invalid";

                //Act 
                var result = new MxSouthAfricanIdCard().Populate(scannedString);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxSouthAfricanIdCardError.InvalidScan));
            }
        }

    }
}
