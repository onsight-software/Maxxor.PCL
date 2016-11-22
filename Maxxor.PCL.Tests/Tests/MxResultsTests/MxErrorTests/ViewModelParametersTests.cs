using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ViewModelParametersTests : BaseUnitTest
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
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            Assert.That(result["exceptionMessage"].Substring(0,20), Is.EqualTo("Object reference not"));
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
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            Assert.That(result["exceptionMessage"], Is.EqualTo(""));
        }

        [Test]
        public void WHEN_there_is_NO_inner_exception_SHOULD_return_blank_InnerExceptionMessage()
        {
            //Arrange
            var sut = new MxErrorBuilder()
                .With_Class("SourceErrorClass")
                .With_ErrorData("key1", "value1")
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Method("SourceMethodName")
                .Create();

            //Act
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            Assert.That(result["innerExceptionMessage"], Is.EqualTo(""));
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
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            Assert.That(result["ErrorCondition"], Is.EqualTo("Crash"));
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
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            Assert.That(result["description"], Is.EqualTo(MxErrorCondition.Crash.Description));
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
            var parameters = sut.ViewModelParameters;
            var result = ParseParameters(parameters);

            //Assert
            const string expectedStack = "ViewModelParametersTests.UpdateError3()\n" +
                                         "ViewModelParametersTests.UpdateError2()\n" +
                                         "ViewModelParametersTests.UpdateError1()\n" +
                                         "SourceErrorClass.SourceErrorMethod()\n";

        Assert.That(result["errorStack"], Is.EqualTo(expectedStack));
        }



        private MxError UpdateError1(MxError error) => MxError.Update(this, error, "UpdateError1");
        private MxError UpdateError2(MxError error) => MxError.Update(this, error, "UpdateError2");
        private MxError UpdateError3(MxError error) => MxError.Update(this, error, "UpdateError3");
        


        private Dictionary<string, string> ParseParameters(object paramsObject)
        {
            var type = paramsObject.GetType();
            var props = type.GetProperties();
            return props.ToDictionary(x => x.Name, x => x.GetValue(paramsObject, null).ToString());
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