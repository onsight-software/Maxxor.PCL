using System;
using Maxxor.PCL.Tests.Tests.Base;
using Maxxor.PCL.ValueObject;
using Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.MxValueObjectsTests.MxSouthAfricanIdNumberTests
{
    [TestFixture]
    public class PopulateTests : BaseUnitTest
    {
        [TestFixture]
        public class DateOfBirthTests : PopulateTests
        {
            [TestCase("7408025069087", 2)]
            [TestCase("7408015069089", 1)]
            [TestCase("8912221087081", 22)]
            public void WHEN_DateOfBirth_Day_is_valid_SHOULD_set_DateOfBirth_Day(string idNumber, int expectedDay)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.DateOfBirth.Day, Is.EqualTo(expectedDay));
            }

            [TestCase("8912331087081")]
            [TestCase("8912aa1087081")]
            public void WHEN_DateOfBirth_Day_is_NOT_valid_SHOULD_return_FAIL(string idNumber)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxSouthAfricanIdNumberError.InvalidDateOfBirth));
                Assert.That(result.Error.SourceError.AdditionalData["idNumber"], Is.EqualTo(idNumber));
            }

            [TestCase("7408025069087", 8)]
            [TestCase("7401015069084", 1)]
            [TestCase("8912221087081", 12)]
            public void WHEN_DateOfBirth_Month_is_valid_SHOULD_set_DateOfBirth_Month(string idNumber, int expectedDay)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.DateOfBirth.Month, Is.EqualTo(expectedDay));
            }

            [TestCase("8913331087081")]
            [TestCase("89aa221087081")]
            public void WHEN_DateOfBirth_Month_is_NOT_valid_SHOULD_return_FAIL(string idNumber)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxSouthAfricanIdNumberError.InvalidDateOfBirth));
                Assert.That(result.Error.SourceError.AdditionalData["idNumber"], Is.EqualTo(idNumber));
            }

            [TestCase("7408025069087", 1974)]
            [TestCase("8912221087081", 1989)]
            public void WHEN_DateOfBirth_Year_is_between_11_and_99_SHOULD_set_DateOfBirth_Year_to_19th_Century(
                string idNumber, int expectedDay)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.DateOfBirth.Year, Is.EqualTo(expectedDay));
            }

            [TestCase("1001015069088", 2010)]
            [TestCase("0401015069081", 2004)]
            public void WHEN_DateOfBirth_Year_is_between_00_and_10_SHOULD_set_DateOfBirth_Year_to_20th_Century(
                string idNumber, int expectedDay)
            {

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.DateOfBirth.Year, Is.EqualTo(expectedDay));
            }
        }

        [TestFixture]
        public class GenderTests : PopulateTests
        {
            [TestCase("7408020000087")]
            [TestCase("7408021111081")]
            [TestCase("7408024999086")]
            public void WHEN_gender_number_is_between_0_and_4999_SHOULD_set_Gender_to_Female(string idNumber)
            {
                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.Gender, Is.EqualTo(MxGender.Female));
            }

            [TestCase("7408025000082")]
            [TestCase("7408026111086")]
            [TestCase("7408029999081")]
            public void WHEN_gender_number_is_between_5000_and_9999_SHOULD_set_Gender_to_Male(string idNumber)
            {
                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.Gender, Is.EqualTo(MxGender.Male));
            }

        }

        [TestFixture]
        public class CitizenshipTests : PopulateTests
        {
            [TestCase("7408025069087")]
            public void WHEN_citizenship_number_is_0_SHOULD_set_Citzienship_true(string idNumber)
            {
                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.IsCitizen, Is.EqualTo(true));
                Assert.That(result.Value.IsPermanentResident, Is.EqualTo(false));
            }

            [TestCase("7408025069186")]
            public void WHEN_citizenship_number_is_1_SHOULD_set_Citzienship_false(string idNumber)
            {
                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.IsCitizen, Is.EqualTo(false));
                Assert.That(result.Value.IsPermanentResident, Is.EqualTo(true));
            }

        }

        [TestFixture]
        public class ChecksumTests : PopulateTests
        {
            [TestCase("7408025069087")]
            [TestCase("7108151062183")]
            [TestCase("0911200713087")]
            public void WHEN_id_number_is_valid_SHOULD_set_IdNumberString(string idNumber)
            {
                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Value.IdNumberString, Is.EqualTo(idNumber));
            }

            [Test]
            public void WHEN_id_number_is_NOT_valid_SHOULD_Fail()
            {
                //Arrange
                const string idNumber = "7408025069187";

                //Act 
                var result = new MxSouthAfricanIdNumber().Populate(idNumber);

                //Assert
                Assert.That(result.Error.ErrorCondition, Is.EqualTo(MxSouthAfricanIdNumberError.InvalidChecksum));
                Assert.That(result.Error.SourceError.AdditionalData["idNumber"], Is.EqualTo(idNumber));
                Assert.That(result.Error.SourceError.AdditionalData["expectedChecksum"], Is.EqualTo(6.ToString()));
            }



        }


    }
}
