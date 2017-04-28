using System.Diagnostics.CodeAnalysis;
using Maxxor.PCL.Enum.Base;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxEnumTests.BaseTests
{
    [TestFixture, ExcludeFromCodeCoverage]
    public class EqualsTests : BaseUnitTest
    {

        [Test]
        public void Objects_with_same_value_SHOULD_be_Equal()
        {
            //Arrange
            var type1 = TestEnum1.Value1;
            var type2 = TestEnum1.Value1;

            //Act
            var result = type1 == type2;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Objects_with_different_values_SHOULD_not_be_Equal()
        {
            //Arrange
            var type1 = TestEnum1.Value1;
            var type2 = TestEnum1.Value2;

            //Act
            var result = type1 == type2;

            //Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void Identical_objects_SHOULD_be_Equal()
        {
            //Arrange
            var sut = TestEnum1.Value1;

            //Act
            var result = sut == sut;

            //Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void Different_Values_SHOULD_not_be_Equal()
        {
            //Arrange
            //Act
            var result = TestEnum1.Value1 == TestEnum1.Value2;

            //Assert
            Assert.That(result, Is.False);
        }
        
    }

    public class TestEnum1 : MxEnum<TestEnum1>
    {
        public TestEnum1(string name) : base(name)
        {
        }

        public static readonly TestEnum1 Value1 = new TestEnum1(nameof(Value1));
        public static readonly TestEnum1 Value2 = new TestEnum1(nameof(Value2));
    }

    public class TestEnum2 : MxEnum<TestEnum2>
    {
        public TestEnum2(string name) : base(name)
        {
        }

        public static readonly TestEnum2 Value1 = new TestEnum2(nameof(Value1));
        public static readonly TestEnum2 Value2 = new TestEnum2(nameof(Value2));
    }


}