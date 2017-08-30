using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxDateTests
{
    public class ToStringTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_return_DD_MM_YYYY()
        {
            //Arrange
            var sut = new MxDate(11,1,2222);

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("11-1-2222"));
        }
        
    }
}
