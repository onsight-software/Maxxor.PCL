using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Builders.MxResultBuilders;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxErrorTests
{
    [TestFixture]
    public class UpdateTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_set_PreviousError()
        {
            //Arrange
            var previousError = new MxErrorBuilder().With_Class("class").Create();
            var sut = MxError.Update(this, previousError);

            //Act
            var result = sut.PreviousError;

            //Assert
            Assert.That(result, Is.EqualTo(previousError));
        }

        [Test]
        public void SHOULD_set_ClassName_automatically()
        {
            //Arrange
            var previousError = new MxErrorBuilder().With_Class("class").Create();
            var sut = MxError.Update(this, previousError);

            //Act
            string result = sut.ClassName;

            //Assert
            Assert.That(result, Is.EqualTo("UpdateTests"));
        }

        [Test]
        public void SHOULD_set_calling_method_name()
        {
            //Arrange
            var previousError = new MxErrorBuilder().With_Class("class").Create();
            var sut = MxError.Update(this, previousError, "meth");

            //Act
            var result = sut.MethodName;

            //Assert
            Assert.That(result, Is.EqualTo("meth"));
        }
    }
}