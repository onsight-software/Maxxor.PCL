using System;
using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ToStringTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_append_ErrorCondition_name()
        {
            //Arrange
            var sut = new MxErrorBuilder().With_MxErrorCondition(MxErrorCondition.Crash).Create();

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("Crash"));
        }

        [Test]
        public void WHEN_source_error_has_an_exception_SHOULD_append_Exception_message()
        {
            //Arrange
            var sourceError = new MxErrorBuilder()
                .With_MxErrorCondition(MxErrorCondition.Crash)
                .With_Exception(new ArgumentNullException())
                .Create();
            var sut = MxError.Update(this, sourceError);

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("Crash (Value cannot be null.)"));
        }
    }
}