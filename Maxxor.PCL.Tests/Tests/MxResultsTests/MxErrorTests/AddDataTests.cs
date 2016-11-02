using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture]
    public class AddDataTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_add_key_value_pair_to_AdditionalData()
        {
            //Arrange
            var sut = new MxErrorBuilder().Create();

            //Act
            var result = sut.AddData("name", "Boris");

            //Assert
            Assert.That(result.AdditionalData.ContainsKey("name"));
            Assert.That(result.AdditionalData["name"], Is.EqualTo("Boris"));
        }
    }
}