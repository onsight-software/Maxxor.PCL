using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxGpsCoordinatesTests
{
    [TestFixture]
    public class TryParseTests : BaseUnitTest
    {
        [Test]
        public void WHEN_parsing_null_SHOULD_return()
        {
            //Arrange

            //Act
            var result = MxGpsCoordinates.TryParse(null);
            //Assert
            Assert.Null(result);
        }

        [Test]
        public void WHEN_parsing_invalid_format_SHOULD_return_null()
        {
            //Arrange
            var str = AnyDouble+";"+AnyDouble;
            //Act
            var result = MxGpsCoordinates.TryParse(str);
            //Assert
            Assert.Null(result);
        }

        [Test]
        public void WHEN_parsing_correct_format_but_invalid_String_SHOULD_return_null()
        {
            //Arrange
            var str = "A place, next to that other place";
            //Act
            var result = MxGpsCoordinates.TryParse(str);
            //Assert
            Assert.Null(result);
        }
        [Test]
        public void WHEN_parsing_correct_format_but_invalid_coords_SHOULD_return_Invalid_Location()
        {
            //Arrange
            var str = 1611.458.ToString(CultureInfo.InvariantCulture)+","+AnyDouble.ToString(CultureInfo.InvariantCulture);
            //Act
            var result = MxGpsCoordinates.TryParse(str);
            //Assert
            Assert.That(!result.IsValidCoordinates);
        }

        [Test]
        public void WHEN_parsing_correct_coords_SHOULD_return_Valid_Location()
        {
            //Arrange
            var str = "41.40338, 2.17403";
            //Act
            var result = MxGpsCoordinates.TryParse(str);
            //Assert
            Assert.That(result.IsValidCoordinates);
        }
    }
}
