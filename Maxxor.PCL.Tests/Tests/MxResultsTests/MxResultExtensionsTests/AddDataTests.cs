using System.Threading.Tasks;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Helpers;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultExtensionsTests
{
    [TestFixture]
    public class AddDataTests : BaseUnitTest
    {
        [TestFixture]
        public class MxResult : BaseUnitTest
        {
            [Test]
            public void SHOULD_Add_Data_to_Error()
            {
                //Arrange
                var key = MxGenerator.AnyString;
                var value = MxGenerator.AnyString;
                var sut = new MxResultBuilder().With_IsSuccess(false).Create();

                //Act
                var result = sut.AddData(key, value);

                //Assert
                Assert.That(result.Error.AdditionalData[key], Is.EqualTo(value));
            }
        }

        [TestFixture]
        public class MxResultOfType : BaseUnitTest
        {
            [Test]
            public void SHOULD_Add_Data_to_Error()
            {
                //Arrange
                var key = MxGenerator.AnyString;
                var value = MxGenerator.AnyString;
                var sut = new MxResultOfTypeBuilder<int>().With_IsSuccess(false).Create();

                //Act
                var result = sut.AddData(key, value);

                //Assert
                Assert.That(result.Error.AdditionalData[key], Is.EqualTo(value));
            }
        }
    }
}