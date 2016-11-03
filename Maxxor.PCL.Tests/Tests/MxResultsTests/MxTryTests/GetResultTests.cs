using System;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxTryTests
{
    public class GetResultTests
    {
        [TestFixture]
        public class TryAction : BaseUnitTest
        {

            [Test]
            public void WHEN_operation_throws_exception_SHOULD_fail_with_Exception_added_to_Error()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Action badMethod = () => { throw exception; };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Action badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Action badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxTry.GetResult(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_operation_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Action goodMethod = () => { };

                //Act
                var sut = MxTry.GetResult(this, goodMethod, MxErrorCondition.Crash);

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
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.IsFailure);
                Assert.That(sut.Error.Exception.Message, Is.EqualTo(exceptionMessage));
                Assert.That(sut.Error.Exception, Is.EqualTo(exception));
            }

            [Test]
            public void WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_class_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<int> badMethod = () => { throw exception; };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.ClassName, Is.EqualTo(GetType().Name));
            }

            [Test]
            public void WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action()
            {
                //Arrange
                const string exceptionMessage = "bad things";
                var exception = new Exception(exceptionMessage);
                Func<int> badMethod = () => { throw exception; };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.SourceError.MethodName,
                    Is.EqualTo("WHEN_operation_throws_exception_SHOULD_set_sender_to_be_the_method_trying_the_action"));
            }

            [Test]
            public void WHEN_error_condition_is_not_specified_SHOULD_set_to_Unspecified()
            {
                //Arrange
                Func<int> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxTry.GetResult(this, badMethod);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Unspecified));
            }

            [Test]
            public void WHEN_error_condition_is_specified_SHOULD_set_it()
            {
                //Arrange
                Func<int> badMethod = () => { throw new Exception(); };

                //Act
                var sut = MxTry.GetResult(this, badMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
            }

            [Test]
            public void WHEN_operation_does_not_throw_exception_SHOULD_return_Ok()
            {
                //Arrange
                Func<int> goodMethod = () => 1;

                //Act
                var sut = MxTry.GetResult(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.IsSuccess);
            }

            [Test]
            public void WHEN_operation_does_not_throw_exception_SHOULD_return_func_return_value_as_Result_Value()
            {
                //Arrange
                Func<int> goodMethod = () => 1;

                //Act
                var sut = MxTry.GetResult(this, goodMethod, MxErrorCondition.Crash);

                //Assert
                Assert.That(sut.Value, Is.EqualTo(1));
            }
        }
    }
}