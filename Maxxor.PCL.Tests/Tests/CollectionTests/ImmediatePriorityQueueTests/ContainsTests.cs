using System.Linq;
using System.Threading.Tasks;
using Maxxor.PCL.Collections;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.ImmediatePriorityQueueTests
{
    [TestFixture]
    public class ContainsTests : BaseUnitTest
    {
        public const int LargeSetSize = 20000;

        [Test]
        public void WHEN_elements_are_added_SHOULD_return_true()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            queue.TryAdd(1);
            
            //Act
            
            //Assert
            Assert.That(queue.Contains(1));
        }

        [Test]
        public void WHEN_elements_are_NOT_add_SHOULD_return_false()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            queue.TryAdd(1);

            //Act

            //Assert
            Assert.False(queue.Contains(2));
        }
        [Test]
        public async Task WHEN_Contain_performed_on_different_thread_to_removes_SHOULD_return_true_for_some_elements()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            
            for (var i = 0; i < LargeSetSize; i++)
            {
                queue.TryAdd(i);
            }
            var removeTask = Task.Run(() =>
            {
                for (var i = 0; i < LargeSetSize / 2; i++)
                {
                    queue.TryTake(out int x);
                }
            });

            //Act
            var queueContainedElementThatWasRemoved = queue.Contains(LargeSetSize / 2 - 1);
            await removeTask;

            //Assert
            Assert.That(queueContainedElementThatWasRemoved);
            Assert.False(queue.Contains(LargeSetSize / 2 - 1));
            for (var i = LargeSetSize / 2; i < LargeSetSize; i++)
            {
                Assert.That(queue.Contains(i));
            }
        }
    }
}
