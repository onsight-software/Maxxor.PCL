using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class TryTests : BaseUnitTest
    {
        [TestFixture]
        public class TryAction : BaseUnitTest
        {

            [Test]
            public void WHEN_action_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Action badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Action badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_action_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Action goodMethod = () => { };

                //Act
                var sut = MxResult.Try(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }
        }

        [TestFixture]
        public class TryFunc : BaseUnitTest
        {
            [Test]
            public void WHEN_func_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<int> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<int> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<int> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<int> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<int> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_action_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<int> goodMethod = () => 1;

                //Act
                var sut = MxResult.Try(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }

            [Test]
            public void WHEN_action_does_not_throw_exception_SHOULD_return_func_return_value_as_Result_Value()
            {
                //Arrange
                Func<int> goodMethod = () => 1;

                //Act
                var sut = MxResult.Try(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Value, Is.EqualTo(1));
            }
        }

        [TestFixture]
        public class TryAsyncFunc : BaseUnitTest
        {
            [Test]
            public void WHEN_func_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<Task> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<Task> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public async Task WHEN_action_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<Task> goodMethod = async () => await Task.Delay(1011000);

                //Act
                var sut = MxResult.Try(this, goodMethod, MxErrorCondition.Crash);
                await Task.Delay(5000);

                //Assert
                Assert.That(sut.IsSuccess);
            }
            
        }

        [TestFixture]
        public class TryAsyncFuncWithReturnValue : BaseUnitTest
        {
            [Test]
            public void WHEN_func_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try<Task<int>>(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try<Task<int>>(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<Task<int>> badMethod = () => { throw exception; };

                //Act
                var sut = MxResult.Try<Task<int>>(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_action_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<Task<int>> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try<Task<int>>(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<Task<int>> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxResult.Try<Task<int>>(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_action_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<Task<int>> goodMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = MxResult.Try<Task<int>>(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }

            [Test]
            public async Task WHEN_action_does_not_throw_exception_SHOULD_set_Result_Value_to_return_value()
            {
                //Arrange
                Func<Task<int>> goodMethod = async () =>
                {
                    await Task.Delay(0);
                    return 1;
                };

                //Act
                var sut = MxResult.Try<Task<int>>(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Value.Result, Is.EqualTo(1));
            }

        }
    }
}   