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
    public class CopyToTests : BaseUnitTest
    {
        public const int MediumSetSize = 1000;
        [Test]
        public void SHOULD_CopyTo_typed_array()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            var nums = new int[MediumSetSize];
            var test = new int[MediumSetSize];
            for (var i = 0; i < MediumSetSize; i++)
            {
                nums[i] = i;
                queue.TryAdd(i);
            }
            //Act
            queue.CopyTo(test,0);
            //Assert
            Assert.That(nums.SequenceEqual(test));
        }
        [Test]
        public void SHOULD_CopyTo_object_array()
        {
            //Arrange
            var queue = new ImmediatePriorityQueue<int>();
            var nums = new object[MediumSetSize];
            var test = new object[MediumSetSize];
            for (var i = 0; i < MediumSetSize; i++)
            {
                nums[i] = i;
                queue.TryAdd(i);
            }
            //Act
            queue.CopyTo(test, 0);
            //Assert
            Assert.That(nums.SequenceEqual(test));
        }
    }
}
