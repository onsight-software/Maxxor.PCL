using System;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture]
    public class ErrorDetailsTests
    {
        [Test]
        public void WHEN_there_is_an_exception_SHOULD_include_it_as_ExceptionMessage()
        {
            //Arrange
            var exception = GenerateException();
            var sut = new MxErrorBuilder()
                .With_Exception(exception)
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var details = sut.ErrorDetails;

            //Assert
            Assert.That(details.ExceptionMessage.Substring(0,20), 
                        Is.EqualTo("Object reference not"));
        }

        [Test]
        public void WHEN_there_is_an_exception_SHOULD_include_StackTrace()
        {
            //Arrange
            var exception = GenerateException();
            var sut = new MxErrorBuilder()
                .With_Exception(exception)
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var details = sut.ErrorDetails;

            //Assert
            Assert.That(details.ExceptionStackTrace.Substring(0, 20),
                        Is.EqualTo("  at Maxxor.PCL.Test"));
        }


        [Test]
        public void WHEN_there_is_NO_exception_SHOULD_return_blank_ExceptionMessage()
        {
            //Arrange
            var sut = new MxErrorBuilder()
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var details = sut.ErrorDetails;

            //Assert
            Assert.That(details.ExceptionMessage, Is.EqualTo(""));
        }

        [Test]
        public void WHEN_there_is_an_inner_exception_SHOULD_include_it_as_ExceptionMessage()
        {
            //Arrange
            var exception = GenerateExceptionWithInnerException();
            var sut = new MxErrorBuilder()
                .With_Exception(exception)
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var details = sut.ErrorDetails;

            //Assert
            Assert.That(details.InnerExceptionMessage.Substring(0, 20),
                        Is.EqualTo("Object reference not"));
        }

        [Test]
        public void WHEN_there_is_NO_inner_exception_SHOULD_return_blank_ExceptionMessage()
        {
            //Arrange
            var exception = GenerateException();
            var sut = new MxErrorBuilder()
                .With_Exception(exception)
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var details = sut.ErrorDetails;

            //Assert
            Assert.That(details.InnerExceptionMessage, Is.EqualTo(""));
        }

        [Test]
        public void SHOULD_include_MxErrorType()
        {
            //Arrange
            var sut = new MxErrorBuilder()
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var result = sut.ErrorDetails;

            //Assert
            Assert.That(result.ErrorType, Is.EqualTo("Crash"));
        }

        [Test]
        public void SHOULD_include_Description()
        {
            //Arrange
            var sut = new MxErrorBuilder()
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var result = sut.ErrorDetails;

            //Assert
            Assert.That(result.Description, Is.EqualTo(MxErrorCondition.Crash.Description));
        }

        [Test]
        public void SHOULD_include_ErrorStack_sources()
        {
            //Arrange
            var error1 = new MxErrorBuilder().With_Class("SourceErrorClass").With_Method("SourceErrorMethod").Create();
            var error2 = UpdateError1(error1);
            var error3 = UpdateError2(error2);
            var sut = UpdateError3(error3);


            //Act
            var result = sut.ErrorDetails;

            //Assert
            const string expectedStack = 
                "ErrorDetailsTests.UpdateError3()\n" +
                "ErrorDetailsTests.UpdateError2()\n" +
                "ErrorDetailsTests.UpdateError1()\n" +
                "SourceErrorClass.SourceErrorMethod()\n";

            Assert.That(result.ErrorStack, Is.EqualTo(expectedStack));
        }

        private MxError UpdateError1(MxError error) => MxError.Update(this, error, "UpdateError1");
        private MxError UpdateError2(MxError error) => MxError.Update(this, error, "UpdateError2");
        private MxError UpdateError3(MxError error) => MxError.Update(this, error, "UpdateError3");
        

        private Exception GenerateExceptionWithInnerException()
        {
            Exception exception = new Exception();
            try
            {
                object m = null;
                string s = m.ToString();
            }
            catch (Exception e)
            {
                exception = new Exception(e.Message, GenerateException());
            }
            return exception;
        }

        private Exception GenerateException()
        {
            Exception exception = new Exception();
            try
            {
                object m = null;
                string s = m.ToString();
            }
            catch (Exception e)
            {
                exception = e;
            }
            return exception;
        }
    }
}
