using System.Collections.Generic;
using Maxxor.PCL.Extensions;
using Maxxor.PCL.Result;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.ListExtensions
{
    [TestFixture]
    public class GetTypesTests : BaseUnitTest
    {
        [Test]
        public void SHOULD_return_list_of_types()
        {
            //Arrange
            var sut = new List<object>
            {
                "hello",
                MxErrorCondition.Unspecified
            };

            //Act
            var result = sut.GetTypes();

            //Assert
            Assert.That(result[0], Is.EqualTo(typeof(string)));
            Assert.That(result[1], Is.EqualTo(typeof(MxErrorCondition)));

        }

    }
}