using Maxxor.PCL.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.SparseMatrixTests
{
    [TestFixture]
    public class RemoveAtTests
    {
        
        [Test]
        public void SHOULD_remove_value_at_xy_position()
        {
            //Arrange
            var sut = new MxSparseMatrix<string>();
            sut.SetAt(10, 15, "me");

            //Act
            sut.RemoveAt(10, 15);

            //Assert
            Assert.That(sut[10, 15], Is.EqualTo(null));
        }
    }
}