using System;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxDateTests
{
    public class ConstructorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_properties()
        {
            //Arrange
            var sut = new MxDate(11, 1, 2222);

            //Assert
            Assert.That(sut.Day, Is.EqualTo(11));
            Assert.That(sut.Month, Is.EqualTo(1));
            Assert.That(sut.Year, Is.EqualTo(2222));
        }

        [Test]
        public void WHEN_day_is_greater_than_31_SHOULD_fail()
        {
            //Arrange
            var result = Assert.Throws<ArgumentException>(() => new MxDate(32,12,1111));

            //Assert
            Assert.That(result.Message, Is.EqualTo("Day cannot be greater than 31"));
        }

        [Test]
        public void WHEN_Month_is_greater_than_12_SHOULD_fail()
        {
            //Arrange
            var result = Assert.Throws<ArgumentException>(() => new MxDate(2, 13, 1111));

            //Assert
            Assert.That(result.Message, Is.EqualTo("Month cannot be greater than 12"));
        }

    }
}
