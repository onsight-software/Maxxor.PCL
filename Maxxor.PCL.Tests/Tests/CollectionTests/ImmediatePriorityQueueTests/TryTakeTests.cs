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
    public class TryTakeTests : BaseUnitTest
    {
        public const int MediumSetSize = 1000;

        [Test]
        public void WHEN_queue_is_empty_TryTake_SHOULD_return_false()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            int x;
            //Act
            var result = queue.TryTake(out x);
            //Assert
            Assert.False(result);
        }
        [Test]
        public void SHOULD_remove_items_in_FIFO_order()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            //Act
            var items = new int[MediumSetSize];
            for (var i = 0; i < MediumSetSize; i++)
            {
                items[i] = MyFixture.Create<int>();
                queue.TryAdd(items[i]);
            }
            int x;
            //Assert
            foreach (var item in items)
            {
                Assert.True(queue.TryTake(out x));
                Assert.AreEqual(item, x);
            }
            Assert.False(queue.TryTake(out x));
            Assert.AreEqual(0, queue.Count);
        }
    }
}
