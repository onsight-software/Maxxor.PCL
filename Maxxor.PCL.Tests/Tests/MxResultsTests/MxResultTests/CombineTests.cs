using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Maxxor.PCL.MxResults;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxResultsTests.MxResultTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class CombineTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_return_Success_for_List_of_successes()
        {
            //Arrange
            var list = new List<MxResult<int>> { MxResult.Ok(1), MxResult.Ok(2), MxResult.Ok(3) };

            //Act
            var sut = MxResult.Combine(list);

            //Assert
            Assert.That(sut.IsSuccess);
            Assert.AreEqual(typeof(MxResult<List<int>>), sut.GetType());
            Assert.AreEqual(typeof(List<int>), sut.Value.GetType());
            Assert.That(sut.Value.SequenceEqual(new[] { 1, 2, 3 }));
        }

        [Test]
        public void SHOULD_return_Failure_for_list_containing_failure()
        {
            //Arrange
            var list = new List<MxResult<int>>()
            {
                MxResult.Ok(1),
                MxResult.Fail<int>(this, MxErrorCondition.Cancelled),
                MxResult.Fail<int>(this, MxErrorCondition.Crash)
            };

            //Act
            var sut = MxResult.Combine(list);

            //Assert
            Assert.That(sut.IsFailure);
            Assert.That(sut.Error.ErrorCondition, Is.EqualTo(MxErrorCondition.Cancelled));
        }

        [Test]
        public void SHOULD_return_Success_for_empty_list_with_empty_list()
        {
            //Arrange
            var list = new List<MxResult<int>>();

            //Act
            var sut = MxResult.Combine(list);

            //Assert
            Assert.That(sut.IsSuccess);
            Assert.AreEqual(typeof(MxResult<List<int>>), sut.GetType());
            Assert.AreEqual(typeof(List<int>), sut.Value.GetType());
            Assert.That(sut.Value, Is.Empty);
        }
    }
}