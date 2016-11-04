using System;
using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class FailTests : BaseUnitTest
    {
        [TestFixture]
        public class Fail : BaseUnitTest
        {
            [Test]
            public void SHOULD_set_IsSuccess_false()
            {
                //Arrange
                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled);

                //Assert
                Assert.That(!result.IsSuccess);
            }

            [Test]
            public void SHOULD_set_IsFailure_true()
            {
                //Arrange
                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled);

                //Assert
                Assert.That(result.IsFailure);
            }

            [Test]
            public void WHEN_given_ErrorType_SHOULD_create_Error_of_that_type()
            {
                //Arrange
                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Cancelled));
            }

            [Test]
            public void WHEN_given_Exception_SHOULD_set_Error_Exception()
            {
                //Arrange
                var exception = new ArgumentNullException();

                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled, exception);

                //Assert
                Assert.That(result.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_given_previous_result_SHOULD_update_Error()
            {
                //Arrange
                var previousResult = new MxResultBuilder().With_Sender(new OkTests()).Create();

                //Act
                var result = MxResult.Fail(this, previousResult);

                //Assert
                Assert.That(result.Error.ClassName, Is.EqualTo(this.GetType().Name));
                Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("OkTests"));
            }

            [Test]
            public void SHOULD_determine_Error_ClassName_from_sender()
            {
                //Arrange
                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled);

                //Assert
                Assert.That(result.Error.ClassName, Is.EqualTo(this.GetType().Name));
            }

            [Test]
            public void SHOULD_determine_Error_MethodName_automatically()
            {
                //Arrange
                //Act
                var result = MxResult.Fail(this, MxErrorCondition.Cancelled);

                //Assert
                Assert.That(result.Error.MethodName, Is.EqualTo("SHOULD_determine_Error_MethodName_automatically"));
            }
        }

        [TestFixture]
        public class FailOfType : BaseUnitTest
        {
            [Test]
            public void SHOULD_set_IsSuccess_false()
            {
                //Arrange
                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash);

                //Assert
                Assert.That(!result.IsSuccess);
            }

            [Test]
            public void SHOULD_set_IsFailure_true()
            {
                //Arrange
                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash);

                //Assert
                Assert.That(result.IsFailure);
            }

            [Test]
            public void WHEN_given_ErrorType_SHOULD_create_Error_of_that_type()
            {
                //Arrange
                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_given_Exception_SHOULD_set_Error_Exception()
            {
                //Arrange
                var exception = new ArgumentNullException();

                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash, exception);

                //Assert
                Assert.That(result.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_given_previous_result_SHOULD_update_Error()
            {
                //Arrange
                var previousResult = new MxResultBuilder().With_Sender(new FailTests()).Create();

                //Act
                var result = MxResult.Fail<StringComparer>(this, previousResult);

                //Assert
                Assert.That(result.Error.ClassName, Is.EqualTo(GetType().Name));
                Assert.That(result.Error.SourceError.ClassName, Is.EqualTo("FailTests"));
            }

            [Test]
            public void SHOULD_determine_Error_ClassName_from_sender()
            {
                //Arrange
                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash);

                //Assert
                Assert.That(result.Error.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void SHOULD_determine_Error_MethodName_automatically()
            {
                //Arrange
                //Act
                var result = MxResult.Fail<StringComparer>(this, MxErrorCondition.Crash);

                //Assert
                Assert.That(result.Error.MethodName, Is.EqualTo("SHOULD_determine_Error_MethodName_automatically"));
            }
        }
    }
}