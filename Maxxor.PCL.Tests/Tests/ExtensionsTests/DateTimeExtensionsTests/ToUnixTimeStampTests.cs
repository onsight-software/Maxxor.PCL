using System;
using Maxxor.PCL.Extensions;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.DateTimeExtensionsTests
{
    [TestFixture]
    public class ToUnixTimestampTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_convert_DateTime_to_unix_timestamp()
        {
            //Arrange
            var sut = DateTime.SpecifyKind(new DateTime(2016, 6, 21, 15, 54, 21), DateTimeKind.Utc);

            //Act
            var result = sut.ToUnixTimestamp();

            //Assert
            Assert.That(result, Is.EqualTo(1466524461));
        }
    }
}