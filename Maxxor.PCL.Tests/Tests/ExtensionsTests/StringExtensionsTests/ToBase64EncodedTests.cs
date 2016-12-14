using Maxxor.PCL.Extensions;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.StringExtensionsTests
{
    [TestFixture]
    public class ToBase64EncodedTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_convert_string_to_base64encoded()
        {
            //Arrange
            var sut = "Adrian";

            //Act
            var result = sut.ToBase64Encoded();

            //Assert
            Assert.That(result, Is.EqualTo("QWRyaWFu"));
        }

    }
}