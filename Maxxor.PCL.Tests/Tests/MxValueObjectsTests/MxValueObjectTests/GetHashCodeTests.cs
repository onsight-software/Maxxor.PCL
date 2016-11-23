using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.TestObjects;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxValueObjectTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class GetHashCodeTests : BaseUnitTest
    {
        [Test]
        public void WHEN_properties_are_idential_SHOULD_have_same_HashCode()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.GetHashCode().Equals(sut2.GetHashCode());

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_properties_are_NOT_idential_SHOULD_have_different_HashCode()
        {
            //Arrange
            var sut1 = new Address("a", "d", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.GetHashCode().Equals(sut2.GetHashCode());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_property_is_null_in_one_object_but_not_in_the_other_SHOULD_have_different_HashCode()
        {
            //Arrange
            var sut1 = new Address("a", null, "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.GetHashCode().Equals(sut2.GetHashCode());

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_property_is_null_inboth_objects_SHOULD_have_same_HashCode()
        {
            //Arrange
            var sut1 = new Address("a", null, "c");
            var sut2 = new Address("a", null, "c");

            //Act
            var result = sut1.GetHashCode().Equals(sut2.GetHashCode());

            //Assert
            Assert.That(result, Is.True);
        }
    }
}