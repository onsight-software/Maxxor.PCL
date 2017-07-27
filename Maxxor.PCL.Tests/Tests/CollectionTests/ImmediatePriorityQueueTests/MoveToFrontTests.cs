using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxxor.PCL.Collections;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.ImmediatePriorityQueueTests
{
    [TestFixture]
    public class MoveToFrontTests : BaseUnitTest
    {
        public const int MediumSetSize = 1000;

        [Test]
        public void SHOULD_NOT_move_null_object()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<string>();
            queue.TryAdd("stuff");
            //Act
            var result = queue.MoveToFront(null);
            //Assert
            Assert.False(result);
        }
        [Test]
        public void SHOULD_move_items_to_start()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            var items = new int[MediumSetSize];
            for (var i = 0; i < MediumSetSize; i++)
            {
                items[i] = MyFixture.Create<int>();
                queue.TryAdd(items[i]);
            }
            int x;
            //Act
            for (var i = 0; i < MediumSetSize; i++)
            {
                Assert.True(queue.MoveToFront(items[i]));
            }

            //Assert

            for (var i = MediumSetSize - 1; i >= 0; i--)
            {
                Assert.True(queue.TryTake(out x));
                Assert.AreEqual(items[i], x);
            }
            Assert.False(queue.TryTake(out x));
            Assert.AreEqual(0, queue.Count);
        }

        [Test]
        public void WHEN_move_fails_SHOULD_be_able_to_move_more_items()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            queue.TryAdd(1);
            queue.TryAdd(2);
            queue.TryAdd(3);

            //Act
            var failureMove = queue.MoveToFront(4);

            //Assert
            Assert.False(failureMove);
            Assert.That(queue.MoveToFront(1));
            Assert.That(queue.MoveToFront(2));
            Assert.That(queue.MoveToFront(3));
            var i = -1;
            queue.TryTake(out i);
            Assert.AreEqual(3, i);
            queue.TryTake(out i);
            Assert.AreEqual(2, i);
            queue.TryTake(out i);
            Assert.AreEqual(1, i);
        }
    }
}
