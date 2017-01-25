using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxGpsCoordinatesTests
{
    [TestFixture]
    public class ConstructorTests : BaseUnitTest
    {
        [Test]
        public void WHEN_given_valid_values_SHOULD_set_LatLong_to_valid_values()
        {
            //Arrange
            const double latitude = 65.564865;
            const double longitude = 33.45659;

            //Act
            var location = new MxGpsCoordinates(latitude, longitude);

            //Assert
            Assert.AreEqual(latitude, location.Latitude);
            Assert.AreEqual(longitude, location.Longitude);
            Assert.That(location.IsValidCoordinates);
        }

        [Test]
        public void WHEN_given_invalid_values_SHOULD_set_LatLong_to_NaN()
        {
            //Arrange
            const double latitude = 165.564865; //Invalid since latitude range is [-90, 90]
            const double longitude = 33.45659;

            //Act
            var location = new MxGpsCoordinates(latitude, longitude);

            //Assert
            Assert.That(double.IsNaN(location.Latitude));
            Assert.That(!location.IsValidCoordinates);
        }
    }
}
