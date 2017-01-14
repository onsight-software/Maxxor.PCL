using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.TestObjects;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxValueObjectTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class EqualsTests : BaseUnitTest
    {
        [Test]
        public void WHEN_properties_are_idential_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_properties_are_NOT_idential_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new Address("a", "d", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_property_is_null_in_compared_object_but_not_in_the_object_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", null, "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_property_is_null_in_object_but_not_in_the_compared_object_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new Address("a", null, "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_property_is_null_inboth_objects_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", null, "c");
            var sut2 = new Address("a", null, "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_same_object_is_compared_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");

            //Act
            var result = sut1.Equals(sut1);

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_multiple_objects_are_identical_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "b", "c");
            var sut3 = new Address("a", "b", "c");

            //Act
            var result1 = sut1.Equals(sut2);
            var result2 = sut2.Equals(sut3);
            var result3 = sut2.Equals(sut3);

            //Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            Assert.That(result3, Is.True);
        }

        [Test]
        public void WHEN_identical_objects_are_compared_using_equality_operator_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1 == sut2;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_non_identical_objects_are_compared_using_equality_operator_SHOULD_return_FALSe()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "d", "c");

            //Act
            var result = sut1 == sut2;

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_identical_objects_are_compared_using_inequality_operator_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "b", "c");

            //Act
            var result = sut1 != sut2;

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_non_identical_objects_are_compared_using_inequality_operator_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new Address("a", "d", "c");

            //Act
            var result = sut1 != sut2;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_derived_types_are_not_identical_Equality_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new ExpandedAddress("a", "b", "c", "d");
            var sut2 = new ExpandedAddress("a", "d", "c", "d");

            //Act
            var result = sut1 == sut2;

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_derived_types_are_identical_Equality_SHOULD_return_TRUE()
        {
            //Arrange
            var sut1 = new ExpandedAddress("a", "b", "c", "d");
            var sut2 = new ExpandedAddress("a", "b", "c", "d");

            //Act
            var result = sut1 == sut2;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_other_ValueObject_is_null_SHOULD_return_false()
        {
            //Arrange
            var sut1 = new ExpandedAddress("a", "b", "c", "d");

            //Act
            var result = sut1.Equals(null);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_other_object_is_null_SHOULD_return_false()
        {
            //Arrange
            var sut1 = new ExpandedAddress("a", "b", "c", "d");
            object other = new object();
            other = null;

            //Act
            var result = sut1.Equals(other);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_objects_have_identical_properties_but_different_types_SHOULD_return_FALSE()
        {
            //Arrange
            var sut1 = new Address("a", "b", "c");
            var sut2 = new UnexpandedAddress("a", "b", "c");

            //Act
            var result = sut1.Equals(sut2);

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_non_null_ValueObject_is_compared_with_null_SHOULD_return_true()
        {
            //Arrange
            var sut = new ExpandedAddress("a", "b", "c", "d");

            //Act
            bool result = (sut == null);
            bool notEqualsResult = (sut != null);

            //Assert
            Assert.That(result, Is.False);
            Assert.That(notEqualsResult, Is.True);
        }

        [Test]
        public void WHEN_null_ValueObject_is_compared_with_null_SHOULD_return_true()
        {
            //Arrange
            var sut = new ExpandedAddress("a", "b", "c", "d");
            sut = null;

            //Act
            bool equalsResult = (sut == null);
            bool notEqualsResult = (sut != null);

            //Assert
            Assert.That(equalsResult, Is.True);
            Assert.That(notEqualsResult, Is.False);
        }
    }
}