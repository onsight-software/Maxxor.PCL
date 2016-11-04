using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.MxStrings;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class DescriptionTests : BaseUnitTest
    {
        [Test]
        public void WHEN_Mx_ErrorStrings_contains_string_with_same_name_as_MxErrorType_SHOULD_return_that_string()
        {
            //Arrange
            var sut = new MxErrorBuilder().With_MxErrorType(MxErrorCondition.Crash).Create();

            //Act
            var result = sut.Description;

            //Assert
            Assert.That(result, Is.EqualTo(MxErrorStrings.Crash));
        }
        
    }
}