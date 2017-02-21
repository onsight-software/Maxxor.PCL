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
    }
}
