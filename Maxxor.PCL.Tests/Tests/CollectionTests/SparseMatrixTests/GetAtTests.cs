using Maxxor.PCL.Collections;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Maxxor.PCL.Tests.Tests.CollectionTests.SparseMatrixTests
{
    [TestFixture]
    public class SetAtTests
    {

        [Test]
        public void SHOULD_set_value_at_xy_position()
        {
            //Arrange
            var sut = new MxSparseMatrix<string>();

            //Act
            sut.SetAt(10, 15, "me");

            //Assert
            Assert.That(sut[10, 15], Is.EqualTo("me"));
        }

        [Test]
        public void SHOULD_replace_value_at_xy_position()
        {
            //Arrange
            var sut = new MxSparseMatrix<string>();
            sut.SetAt(10, 15, "me");

            //Act
            sut.SetAt(10, 15, "you");

            //Assert
            Assert.That(sut[10, 15], Is.EqualTo("you"));
        }
    }
}