using System;
using System.Text;

namespace Maxxor.PCL.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64Encoded(this string stringToEncode)
        {
            var stringToEncodeBytes = System.Text.Encoding.UTF8.GetBytes(stringToEncode);
            return System.Convert.ToBase64String(stringToEncodeBytes);
        }

        public static bool IsValidEmail(this string stringToValidate)
        {
            if (string.IsNullOrEmpty(stringToValidate))
                return false;
            return stringToValidate.Contains("@") && stringToValidate.Contains(".");
        }

        public static string ToInitials(this string stringName, int? maxInitials = null)
        {
            var firstLetters = new StringBuilder();
            foreach (var part in stringName.Split(' '))
            {
                if (part?.Length > 0)
                {
                    firstLetters.Append(part.Substring(0, 1).ToUpperInvariant());
                }
            }
            if (maxInitials != null)
            {
                while (firstLetters.Length > maxInitials)
                {
                    var indexOfCharacterToRemove = (int)Math.Round((double)firstLetters.Length/2);
                    firstLetters = firstLetters.Remove(indexOfCharacterToRemove, 1);
                }
            }
            return firstLetters.ToString();
        }
    }
}