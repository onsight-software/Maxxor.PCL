using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class SourceErrorTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_find_first_error_in_the_list()
        {
            //Arrange
            var error1 = MxError.Create(this, MxErrorCondition.Crash);
            error1.AddData("number", "1");
            var error2 = MxError.Update(this, error1);
            var error3 = MxError.Update(this, error2);
            var sut = MxError.Update(this, error3);

            //Act
            var result = sut.SourceError;

            //Assert
            Assert.That(result.AdditionalData["number"], Is.EqualTo("1"));
        }

        [Test]
        public void WHEN_there_are_no_PreviousErrors_SHOULD_return_current_Error()
        {
            //Arrange
            var sut = MxError.Create(this, MxErrorCondition.Crash).AddData("a", "1");

            //Act
            var result = sut.SourceError;

            //Assert
            Assert.That(result.ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
        }

    }
}