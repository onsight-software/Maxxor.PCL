using Maxxor.PCL.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.SparseMatrixTests
{
    [TestFixture]
    public class GetAtTests
    {
        
        [Test]
        public void SHOULD_return_value_at_xy_position()
        {
            //Arrange
            var sut = new MxSparseMatrix<string>();
            sut.SetAt(10, 15, "me");

            //Act
            var result = sut.GetAt(10, 15);

            //Assert
            Assert.That(result, Is.EqualTo("me"));
        }
    }
}