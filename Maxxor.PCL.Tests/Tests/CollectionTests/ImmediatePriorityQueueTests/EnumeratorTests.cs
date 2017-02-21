using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maxxor.PCL.Collections;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.ImmediatePriorityQueueTests
{
    [TestFixture]
    public class EnumeratorTests : BaseUnitTest
    {
        public const int MediumSetSize = 1000;
        [Test]
        public void SHOULD_Enumerate_in_foreach_loop()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            var nums = new int[MediumSetSize];
            for (var i = 0; i < MediumSetSize; i++)
            {
                nums[i] = i;
                queue.TryAdd(i);
            }
            var test = new List<int>();
            //Act
            foreach (var i in queue)
            {
                test.Add(i);
            }

            //Assert
            Assert.That(nums.SequenceEqual(test));
        }
    }
}
