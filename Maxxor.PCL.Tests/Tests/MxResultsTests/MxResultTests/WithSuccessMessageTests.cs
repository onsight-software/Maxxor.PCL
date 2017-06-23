using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Builders.Base;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class WithSuccessMessageTests : BaseUnitTest
    {
        [Test]
        public void WHEN_Result_has_no_type_SHOULD_set_SuccessMessage()
        {
            //Arrange
            //Act
            var result = MxResult.Ok()
                .WithSuccessMessage("Great Stuff!");

            //Assert
            Assert.That(result.SuccessMessage, Is.EqualTo("Great Stuff!"));
        }

        [Test]
        public void WHEN_Result_has_no_type_SHOULD_return_Result()
        {
            //Arrange
            //Act
            var result = MxResult.Ok()
                .WithSuccessMessage("Great Stuff!");

            //Assert
            Assert.That(result, Is.InstanceOf<MxResult>());
        }

        [Test]
        public void WHEN_Result_has_a_type_SHOULD_set_SuccessMessage()
        {
            //Arrange
            //Act
            var result = MxResult.Ok(12)
                .WithSuccessMessage("Great Stuff!");

            //Assert
            Assert.That(result.SuccessMessage, Is.EqualTo("Great Stuff!"));
        }

        [Test]
        public void WHEN_Result_has_a_type_SHOULD_return_Result_of_T_with_Value()
        {
            //Arrange
            //Act
            var result = MxResult.Ok(12)
                .WithSuccessMessage("Great Stuff!");

            //Assert
            Assert.That(result, Is.InstanceOf<MxResult<int>>());
            Assert.That(result.Value, Is.EqualTo(12));
        }

    }
}