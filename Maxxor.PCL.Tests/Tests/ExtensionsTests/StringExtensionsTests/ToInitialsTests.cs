using Maxxor.PCL.Extensions;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.StringExtensionsTests
{
    [TestFixture]
    public class ToInitialsTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_return_first_letter_of_each_separate_substring()
        {
            //Arrange
            var sut = "Adrian Frielinghaus";

            //Act
            var result = sut.ToInitials();

            //Assert
            Assert.That(result, Is.EqualTo("AF"));
        }

        [Test]
        public void SHOULD_return_uppercase_initials()
        {
            //Arrange
            var sut = "Adrian frielinghaus";

            //Act
            var result = sut.ToInitials();

            //Assert
            Assert.That(result, Is.EqualTo("AF"));
        }

        [Test]
        public void IF_string_is_empty_SHOULD_return_empty()
        {
            //Arrange
            var sut = "";

            //Act
            var result = sut.ToInitials();

            //Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void IF_maxInitials_is_set_SHOULD_remove_extra_initials_from_the_inside()
        {
            //Arrange
            var sut = "One Two Three";

            //Act
            var result = sut.ToInitials(2);

            //Assert
            Assert.That(result, Is.EqualTo("OT"));
        }

        [Test]
        public void IF_maxInitials_is_set_SHOULD_remove_extra_initials_from_the_front()
        {
            //Arrange
            var sut = "Ant Bee Crow Dog";

            //Act
            var result = sut.ToInitials(3);

            //Assert
            Assert.That(result, Is.EqualTo("ABD"));
        }

    }
}