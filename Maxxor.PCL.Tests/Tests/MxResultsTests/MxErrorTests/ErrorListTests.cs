using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ErrorListTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_convert_ErrorStack_into_newline_separated_string()
        {
            //Arrange
            var error1 = MxError.Create(this, MxErrorCondition.Crash);
            var sut = MxError.Update(this, error1);

            //Act
            var result = sut.ErrorList;

            //Assert
            Assert.That(result, Is.EqualTo("ErrorListTests.SHOULD_convert_ErrorStack_into_newline_separated_string()\nErrorListTests.SHOULD_convert_ErrorStack_into_newline_separated_string()\n"));
        }
        
    }
}