using Maxxor.PCL.Collections;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.ImmediatePriorityQueueTests
{
    [TestFixture]
    public class TryAddTests : BaseUnitTest
    {
        public const int MediumSetSize = 1000;

        [Test]
        public void SHOULD_NOT_add_null_object()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<string>();
            //Act
            var result = queue.TryAdd(null);
            //Assert
            Assert.False(result);
        }
        [Test]
        public void SHOULD_add_items()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            //Act

            //Assert
            for (var i = 0; i < MediumSetSize; i++)
            {
                Assert.True(queue.TryAdd(MyFixture.Create<int>()));
            }
            Assert.AreEqual(MediumSetSize, queue.Count);
        }

        [Test]
        public void WHEN_add_fails_SHOULD_be_able_to_add_more_items()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            queue.TryAdd(1);
            queue.TryAdd(2);
            var failureAdd = queue.TryAdd(2); 

            //Act
            queue.TryAdd(3);

            //Assert
            Assert.False(failureAdd);
            Assert.That(queue.Contains(1));
            Assert.That(queue.Contains(2));
            Assert.That(queue.Contains(3));
        }
    }
}
