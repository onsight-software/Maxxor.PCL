using System;
using System.Net;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture]
    public class CreateTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_Exception_if_provided()
        {
            //Arrange
            var exception = new WebException();
            var sut = MxError.Create(this, MxErrorCondition.Crash, exception);

            //Act
            Exception result = sut.Exception;

            //Assert
            Assert.That(result, Is.EqualTo(exception));
        }

        [Test]
        public void SHOULD_set_ClassName_from_sender()
        {
            //Arrange
            var sut = MxError.Create(this, MxErrorCondition.Crash);

            //Act
            string result = sut.ClassName;

            //Assert
            Assert.That(result, Is.EqualTo("CreateTests"));
        }

        [Test]
        public void SHOULD_set_calling_method_name()
        {
            //Arrange
            var sut = MxError.Create(this, MxErrorCondition.Crash, null, "methodName");

            //Act
            var result = sut.MethodName;

            //Assert
            Assert.That(result, Is.EqualTo("methodName"));
        }

        [Test]
        public void SHOULD_set_MxErrorType()
        {
            //Arrange
            var sut = MxError.Create(this, MxErrorCondition.Crash);

            //Act
            var result = sut.ErrorCondition;

            //Assert
            Assert.That(result, Is.EqualTo(MxErrorCondition.Crash));
        }
    }
}