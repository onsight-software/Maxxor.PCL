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
    public class CompleteUseTests : BaseUnitTest
    {
        public const int LargeSetSize = 15000;
        [Test]
        public void SHOULD_add_remove_add_item()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            //Act
            int x;
            int y;
            queue.TryAdd(-1);
            queue.TryTake(out x);
            queue.TryAdd(-1);
            //Assert
            Assert.AreEqual(1, queue.Count);
            Assert.True(queue.TryTake(out y));
            Assert.AreEqual(-1, y);
        }
        [Test]
        public void SHOULD_add_remove_items_on_single_thread()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();

            //Act
            for (var i = 0; i < 2 * LargeSetSize; i++)
            {
                queue.TryAdd(MyFixture.Create<int>());
            }
            int x;
            while (queue.TryTake(out x)) { }

            //Assert
            Assert.AreEqual(0, queue.Count);
        }
        [Test]
        public async Task SHOULD_add_remove__reprioritise_items_on_multiple_threads()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            var items1 = new int[LargeSetSize];
            var items2 = new int[LargeSetSize];
            for (var i = 0; i < LargeSetSize; i++)
            {
                items1[i] = MyFixture.Create<int>();
                items2[i] = MyFixture.Create<int>();
            }
            //Act
#pragma warning disable 4014
            var adding1 = Task.Run(() =>

            {
                foreach (var num in items1)
                {
                    queue.TryAdd(num);
                }
            });
            var adding2 = Task.Run(() =>
            {
                foreach (var num in items2)
                {
                    queue.TryAdd(num);
                }
            });
            await adding1;
            var modifying = Task.Run(() =>
            {
                for (var i = 0; i < items1.Length; i++)
                {
                    queue.MoveToFront(items1[i]);
                }
            });
            var removing = Task.Run(() =>
            {
                int x;
                int i = 0;
                var totalSize = items1.Length + items2.Length;
                while (i < totalSize)
                {
                    if (queue.TryTake(out x))
                        i++;
                }
            });
#pragma warning restore 4014
            await removing;

            //Assert
            Assert.AreEqual(0, queue.Count);
        }
    }
}
