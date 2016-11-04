using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.Base;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class OkTests : BaseUnitTest
    {
        [TestFixture]
        public class Ok : BaseMxBuilder
        {
            [Test]
            public void SHOULD_set_IsSuccess_true()
            {
                //Arrange
                //Act
                var result = MxResult.Ok();

                //Assert
                Assert.That(result.IsSuccess);
            }


            [Test]
            public void SHOULD_set_IsFailure_false()
            {
                //Arrange
                //Act
                var result = MxResult.Ok();

                //Assert
                Assert.That(!result.IsFailure);
            }
        }

        [TestFixture]
        public class OkOfType : BaseMxBuilder
        {
            [Test]
            public void SHOULD_set_IsSuccess_true()
            {
                //Arrange
                var value = new StringAssert();

                //Act
                var result = MxResult.Ok(value);

                //Assert
                Assert.That(result.IsSuccess);
            }

            [Test]
            public void SHOULD_set_IsFailure_false()
            {
                //Arrange
                var value = new StringAssert();

                //Act
                var result = MxResult.Ok(value);

                //Assert
                Assert.That(!result.IsFailure);
            }

            [Test]
            public void SHOULD_set_Value()
            {
                //Arrange
                var value = new StringAssert();

                //Act
                var result = MxResult.Ok(value);

                //Assert
                Assert.That(result.Value, Is.EqualTo(value));
            }
        }
    }
}