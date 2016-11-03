using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxTryTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class GetResultAsyncTests
    {

        [TestFixture]
        public class TryAsyncFunc : BaseUnitTest
        {
            [Test]
            public async Task WHEN_func_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public async Task WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<Task> badMethod = () => { throw new Exception(); };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public async Task WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<Task> badMethod = () => { throw new Exception(); };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public async Task WHEN_operation_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<Task> goodMethod = async () => await Task.Delay(0);

                //Act
                var sut = await MxTry.GetResultAsync(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }

        }

        [TestFixture]
        public class TryAsyncFuncWithReturnValue : BaseUnitTest
        {
            [Test]
            public async Task WHEN_func_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = async () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public async Task WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<Task<int>> badMethod = () => { throw new Exception(); };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public async Task WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<Task<int>> badMethod = () => { throw new Exception(); };

                //Act
                var sut = await MxTry.GetResultAsync(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public async Task WHEN_operation_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<Task<int>> goodMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = await MxTry.GetResultAsync(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }

            [Test]
            public async Task WHEN_operation_does_not_throw_exception_SHOULD_set_Result_Value_to_return_value()
            {
                //Arrange
                Func<Task<int>> goodMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = await MxTry.GetResultAsync(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Value, Is.EqualTo(1));
            }

        }

        [TestFixture]
        public class TryAsyncFuncWithReturnValueAndRetry : BaseUnitTest
        {
            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_fail_with_all_Exceptions_aggregated()
            {
                //Arrange
                int numberOfTimesToRetry = 5;
                int numberOfTimesActuallyRun = 0;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () =>
                {
                    numberOfTimesActuallyRun++;
                    throw exception;
                };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                var aggregateExeption = sut.Error.Exception as AggregateException;
                Assert.That(sut.IsFailure);
                Assert.That(numberOfTimesActuallyRun, Is.EqualTo(numberOfTimesToRetry));
                Assert.IsInstanceOf<AggregateException>(sut.Error.Exception);
                Assert.AreEqual(numberOfTimesToRetry, aggregateExeption?.InnerExceptions.Count);
                Assert.That(aggregateExeption != null && aggregateExeption.InnerExceptions.All(x => x.Message.Equals("bad things")));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_append_retry_information_to_the_Error()
            {
                //Arrange
                int numberOfTimesToRetry = 1;
                int retryDelay = 1;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () =>
                {
                    throw exception;
                };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry, retryDelay);

                //Assert
                Assert.That(sut.Error.AdditionalData["tries"], Is.EqualTo("Tried operation 1 times every 1 milliseconds"));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                int numberOfTimesToRetry = 5;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public async Task WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const int numberOfTimesToRetry = 5;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public async Task WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                const int numberOfTimesToRetry = 5;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public async Task WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                const int numberOfTimesToRetry = 5;
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry, 0, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public async Task WHEN_operation_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                const int numberOfTimesToRetry = 5;
                Func<Task<int>> badMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                Assert.That(sut.IsSuccess);
            }

            [Test]
            public async Task WHEN_operation_does_not_throw_exception_SHOULD_set_Result_Value_to_return_value_of_operation()
            {
                //Arrange
                const int numberOfTimesToRetry = 5;
                Func<Task<int>> badMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = await MxTry.GetRetryResult(this, badMethod, numberOfTimesToRetry);

                //Assert
                Assert.That(sut.Value, Is.EqualTo(1));
            }

        }

    }
}