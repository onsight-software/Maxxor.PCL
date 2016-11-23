using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ReturnTests : BaseUnitTest
    {
        [TestFixture]
        public class Result : BaseUnitTest
        {

            [Test]
            public void WHEN_Result_IsSuccess_SHOULD_return_IsSuccess()
            {
                //Arrange
                var previousResult = new MxResultBuilder().With_IsSuccess(true).Create();

                //Act
                var result = MxResult.Return(this, previousResult);

                //Assert
                Assert.That(result.IsSuccess);
            }

            [Test]
            public void WHEN_Result_IsFailure_SHOULD_Update_and_return_Failure()
            {
                //Arrange
                var previousResult = new MxResultBuilder().With_Sender(new OkTests()).With_IsSuccess(false).Create();

                //Act
                var result = MxResult.Return(this, previousResult);

                //Assert
                Assert.That(result.IsFailure);
                Assert.That(result.Error.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(result.Error.SourceError.ClassName, Is.EqualTo(new OkTests().GetType().Name));
            }
        }

        [TestFixture]
        public class ResultOfType : BaseUnitTest
        {
            [Test]
            public void WHEN_Result_IsSuccess_SHOULD_return_IsSuccess()
            {
                //Arrange
                var previousResult = new MxResultOfTypeBuilder<DescriptionAttribute>().With_IsSuccess(true).Create();

                //Act
                var result = MxResult.Return(this, previousResult);

                //Assert
                Assert.That(result.IsSuccess);
            }

            [Test]
            public void WHEN_Result_IsFailure_SHOULD_Update_and_return_Failure()
            {
                //Arrange
                var previousResult = new MxResultOfTypeBuilder<NUnitAttribute>().With_Sender(new OkTests()).With_IsSuccess(false).Create();

                //Act
                var result = MxResult.Return(this, previousResult);

                //Assert
                Assert.That(result.IsFailure);
                Assert.That(result.Error.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(result.Error.SourceError.ClassName, Is.EqualTo(new OkTests().GetType().Name));
            }

        }
    }
}
