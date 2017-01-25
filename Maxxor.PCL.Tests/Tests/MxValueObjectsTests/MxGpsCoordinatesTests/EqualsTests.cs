using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxxor.PCL.Tests.TestObjects;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxGpsCoordinatesTests
{
    public class EqualsTests : BaseUnitTest
    {
        [Test]
        public void WHEN_properties_are_idential_SHOULD_return_TRUE()
        {
            //Arrange
            var latitude = AnyDouble;
            var longitude = AnyDouble;
            var sut1 = new MxGpsCoordinates(latitude, longitude);
            var sut2 = new MxGpsCoordinates(latitude, longitude);

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_properties_are_NOT_idential_SHOULD_return_FALSE()
        {
            //Arrange
            var latitude = AnyDouble;
            var sut1 = new MxGpsCoordinates(latitude, 1);
            var sut2 = new MxGpsCoordinates(latitude, 2);

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.False);
        }
        [Test]
        public void WHEN_properties_are_NaN_SHOULD_return_TRUE()
        {
            //Arrange
            const double latitude = double.NaN;
            const double longitude = double.NaN;
            var sut1 = new MxGpsCoordinates(latitude, longitude);
            var sut2 = new MxGpsCoordinates(latitude, longitude);

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.True);
        }
    }
}
