using Maxxor.PCL.Extensions;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.StringExtensionsTests
{
    [TestFixture]
    public class IsValidEmailTests : BaseUnitTest
    {
        [Test]
        public void WHEN_string_does_not_contain_at_SHOULD_return_false()
        {
            //Arrange
            const string sut = "invalid.";

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_string_does_not_contain_dot_SHOULD_return_false()
        {
            //Arrange
            const string sut = "invalid@";

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_string_contains_dot_and_at_SHOULD_return_true()
        {
            //Arrange
            const string sut = "valid@.";

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void WHEN_string_is_empty_SHOULD_return_false()
        {
            //Arrange
            const string sut = "";

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_string_is_whitespace_SHOULD_return_false()
        {
            //Arrange
            const string sut = "  ";

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void WHEN_string_is_null_SHOULD_return_false()
        {
            //Arrange
            const string sut = null;

            //Act
            var result = sut.IsValidEmail();

            //Assert
            Assert.That(result, Is.False);
        }
    }
}