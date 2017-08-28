using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxDateTests
{
    public class EqualsTests : BaseUnitTest
    {
        [Test]
        public void WHEN_properties_are_idential_SHOULD_return_TRUE()
        {
            //Arrange
            var sutOne = new MxDate(11,1,2222);
            var sutTwo = new MxDate(11,1,2222);

            //Act
            var result = sutOne.Equals(sutTwo);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_day_is_different_SHOULD_return_false()
        {
            //Arrange
            var sutOne = new MxDate(11, 1, 2222);
            var sutTwo = new MxDate(1, 1, 2222);

            //Act
            var result = sutOne.Equals(sutTwo);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_month_is_different_SHOULD_return_false()
        {
            //Arrange
            var sutOne = new MxDate(11, 1, 2222);
            var sutTwo = new MxDate(11, 2, 2222);

            //Act
            var result = sutOne.Equals(sutTwo);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_year_is_different_SHOULD_return_false()
        {
            //Arrange
            var sutOne = new MxDate(11, 1, 2222);
            var sutTwo = new MxDate(11, 1, 2122);

            //Act
            var result = sutOne.Equals(sutTwo);

            //Assert
            Assert.That(result, Is.False);
        }
    }
}
