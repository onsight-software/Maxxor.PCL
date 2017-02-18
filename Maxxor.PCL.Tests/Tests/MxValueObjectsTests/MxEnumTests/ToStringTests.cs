using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using Maxxor.PCL.ValueObject.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxEnumTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class ToStringTests : BaseUnitTest
    {

        [Test]
        public void Objects_with_same_value_SHOULD_be_Equal()
        {
            //Arrange
            var sut = MyEnum.FirstValue;

            //Act
            var result = sut.ToString();

            //Assert
            Assert.That(result, Is.EqualTo("FirstValue"));
        }

        private class MyEnum : MxEnum<MyEnum>
        {
            public MyEnum(string typeName) : base(typeName)
            {
            }

            public static readonly MyEnum FirstValue =  new MyEnum("FirstValue");
        }
    }
}