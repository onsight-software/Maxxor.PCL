using System;
using Maxxor.PCL.Extensions;
using Maxxor.PCL.Resources;
using Maxxor.PCL.Tests.Tests.Base;
using NUnit.Framework;

namespace Maxxor.PCL.Tests.Tests.ExtensionsTests.TimeSpanExtensionsTests
{
    [TestFixture]
    public class ToIntervalStringTests : BaseUnitTest
    {
        [Test]
        public void WHEN_Timespan_is_more_than_365_days_SHOULD_use_years_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(785, 0, 0, 0);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(2 + MxTimeStrings.Timespan_abbreviation_years));
        }

        [Test]
        public void WHEN_Timespan_is_more_than_30_days_SHOULD_use_months_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(55, 0, 0, 0);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(1 + MxTimeStrings.Timespan_abbreviation_months));
        }

        [Test]
        public void WHEN_Timespan_is_between_1_and_30_days_SHOULD_use_days_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(12, 1, 22, 43);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(12 + MxTimeStrings.Timespan_abbreviation_days));
        }

        [Test]
        public void WHEN_Timespan_is_between_1_and_24_hours_SHOULD_use_hours_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(0, 2, 22, 43);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(2 + MxTimeStrings.Timespan_abbreviation_hours));
        }

        [Test]
        public void WHEN_Timespan_is_between_0_and_60_minutes_SHOULD_use_minutes_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(0, 0, 22, 43);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(22 + MxTimeStrings.Timespan_abbreviation_minutes));
        }

        [Test]
        public void WHEN_Timespan_is_between_0_and_60_seconds_SHOULD_use_seconds_and_round_down()
        {
            //Arrange
            var sut = new TimeSpan(0, 0, 0, 43);

            //Act
            var result = sut.ToIntervalString();

            //Assert
            Assert.That(result, Is.EqualTo(43 + MxTimeStrings.Timespan_abbreviation_seconds));
        }

    }
}