using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ErrorStackTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_retrieve_list_of_current_Error_and_all_PreviousErrors()
        {
            //Arrange
            var error1 = MxError.Create(this, MxErrorCondition.Crash);
            var error2 = MxError.Update(this, error1);
            var error3 = MxError.Update(this, error2);
            var sut = MxError.Update(this, error3);

            //Act
            List<MxError> result = sut.ErrorStack;

            //Assert
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result[0].ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
        }

        [Test]
        public void WHEN_there_are_no_PreviousErrors_SHOULD_retrieve_list_of_current_Error_only()
        {
            //Arrange
            var sut = MxError.Create(this, MxErrorCondition.Crash);

            //Act
            var result = sut.ErrorStack;

            //Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
        }


        [Test]
        public void WHEN_there_is_one_PreviousError_SHOULD_retrieve_list_of_2_errors()
        {
            //Arrange
            var error = MxError.Create(this, MxErrorCondition.Crash);
            var sut = MxError.Update(this, error);

            //Act
            var result = sut.ErrorStack;

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].ErrorCondition, Is.EqualTo(MxErrorCondition.Crash));
        }
    }
}