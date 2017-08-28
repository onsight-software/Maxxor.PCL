using System;
using System.Collections.Generic;
using Maxxor.PCL.Result;
using Maxxor.PCL.ValueObject.Base;

namespace Maxxor.PCL.ValueObject.MxSouthAfricanIdNumber
{
    public class MxSouthAfricanIdNumber : MxValueObject<MxSouthAfricanIdNumber>
    {

        public string IdNumberString { get; private set; }

        public MxResult<MxSouthAfricanIdNumber> Populate(string idNumberString)
        {
            var processResults = new List<MxResult>
            {
                PopulateDateOfBirth(idNumberString),
                PopulateGender(idNumberString),
                PopulateCitizenship(idNumberString),
                ValidateChecksum(idNumberString)
            };

            var combinedProcessorResult = MxResult.Combine(this, processResults);

            return combinedProcessorResult.IsFailure 
                ? MxResult.Fail<MxSouthAfricanIdNumber>(this, combinedProcessorResult) 
                : MxResult.Ok(this);
        }

        #region Date Of Birth

        public MxDate DateOfBirth { get; private set; }
        private MxResult PopulateDateOfBirth(string idNumberString)
        {
            var yearSubString = idNumberString.Substring(0, 2);
            var monthSubString = idNumberString.Substring(2, 2);
            var daySubString = idNumberString.Substring(4, 2);

            try
            {
                var day = int.Parse(daySubString);
                var month = int.Parse(monthSubString);
                var year = int.Parse(yearSubString);

                if (year > 0 && year < 11)
                {
                    year += 2000;
                }
                else
                {
                    year += 1900;
                }

                DateOfBirth = new MxDate(day, month, year);
            }
            catch (Exception e)
            {
                var fail = MxResult.Fail(this, MxSouthAfricanIdNumberError.InvalidDateOfBirth, e);
                fail.Error.AddData("idNumber", idNumberString);
                return fail;
            }

            return MxResult.Ok();
        }

        #endregion

        #region Gender

        public MxGender Gender { get; private set; }

        private MxResult PopulateGender(string idNumberString)
        {
            var genderString = idNumberString.Substring(6, 4);
            var genderNumber = int.Parse(genderString);

            if (genderNumber >= 0 && genderNumber < 5000)
            {
                Gender = MxGender.Female;
                return MxResult.Ok();
            }
            if (genderNumber >= 5000 && genderNumber <= 9999)
            {
                Gender = MxGender.Male;
                return MxResult.Ok();
            }

            return MxResult.Fail(this, MxSouthAfricanIdNumberError.InvalidGender)
                .AddData("idNumber", idNumberString); ;

        }

        #endregion

        #region Citizenship

        public bool IsCitizen { get; private set; }
        public bool IsPermanentResident { get; private set; }

        private MxResult PopulateCitizenship(string idNumberString)
        {
            var citizenshipSubstring = idNumberString.Substring(10, 1);

            if (citizenshipSubstring == "0")
            {
                IsCitizen = true;
                IsPermanentResident = false;
            }
            else
            {
                IsCitizen = false;
                IsPermanentResident = true;
            }

            return MxResult.Ok();
        }

        #endregion

        #region Checksum

        private MxResult ValidateChecksum(string idNumberString)
        {
            var checksumString = idNumberString.Substring(12, 1);
            var actualChecksum = int.Parse(checksumString);
            var expectedChecksum = GetChecksumDigit(idNumberString);

            if (actualChecksum == expectedChecksum)
            {
                IdNumberString = idNumberString;
                return MxResult.Ok();
            }

            return MxResult.Fail(this, MxSouthAfricanIdNumberError.InvalidChecksum)
                .AddData("idNumber", idNumberString)
                .AddData("expectedChecksum", expectedChecksum.ToString());
        }

        public int GetChecksumDigit(string idNumberString)
        {
            var checksum = -1;
            try
            {
                var sumOfNumbersInOddPositions = 0;
                for (var i = 0; i < 6; i++)
                {
                    sumOfNumbersInOddPositions += int.Parse(idNumberString[2 * i].ToString());
                }

                var contatenationOfNumbersInEvenPositions = 0;
                for (var i = 0; i < 6; i++)
                {
                    contatenationOfNumbersInEvenPositions = contatenationOfNumbersInEvenPositions * 10 + int.Parse(idNumberString[2 * i + 1].ToString());
                }
                contatenationOfNumbersInEvenPositions *= 2;

                var sumOfNumbersCalculatedFromConcatentation = 0;
                do
                {
                    sumOfNumbersCalculatedFromConcatentation += contatenationOfNumbersInEvenPositions % 10;
                    contatenationOfNumbersInEvenPositions = contatenationOfNumbersInEvenPositions / 10;
                }
                while (contatenationOfNumbersInEvenPositions > 0);

                sumOfNumbersCalculatedFromConcatentation += sumOfNumbersInOddPositions;

                checksum = 10 - sumOfNumbersCalculatedFromConcatentation % 10;
                if (checksum == 10) checksum = 0;

            }
            catch {/*ignore*/}
            return checksum;
        }

        #endregion

    }
}