using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxEnumTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class EqualsTests : BaseUnitTest
    {

        [Test]
        public void Objects_with_same_value_SHOULD_be_Equal()
        {
            //Arrange
            var type1 = TypeType.Type1;
            var type2 = TypeType.Type1;

            //Act
            var result = type1 == type2;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Objects_with_different_values_SHOULD_not_be_Equal()
        {
            //Arrange
            var type1 = TypeType.Type1;
            var type2 = TypeType.Type2;

            //Act
            var result = type1 == type2;

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Identical_objects_SHOULD_be_Equal()
        {
            //Arrange
            var sut = TypeType.Type1;

            //Act
            var result = sut == sut;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Different_Types_SHOULD_not_be_Equal()
        {
            //Arrange
            var sut = TypeType.Type1;

            //Act
            var result = sut == TypeType.Type2;

            //Assert
            Assert.That(result, Is.False);
        }
    }

    public class TypeType : MxEnum
    {
        public string Value { get; }
        public override string ToString()
        {
            return Value;
        }

        public TypeType(string value) : base(value)
        {
            Value = value;
        }

        public static readonly TypeType Type1 = new TypeType("Type1");
        public static readonly TypeType Type2 = new TypeType("Type2");
    }


}