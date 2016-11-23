using System;
using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Builders.Base;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ToStringTests : BaseUnitTest
    {

        [Test]
        public void WHEN_result_is_fail_SHOULD_return_FAIL_plus_Error_ToString()
        {
            //Arrange
            var error = new MxErrorBuilder().With_MxErrorCondition(MxErrorCondition.Crash).With_Exception(new ArgumentNullException()).Create();
            var sut = new MxResultBuilder().With_IsSuccess(false).With_Error(error).Create();
            
            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("FAIL: " + error));
        }

        [Test]
        public void WHEN_result_is_ok_SHOULD_return_OK()
        {
            //Arrange
            var sut = new MxResultBuilder().With_IsSuccess(true).Create();

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("OK"));
        }

    }
}