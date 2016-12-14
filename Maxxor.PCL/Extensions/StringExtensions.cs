namespace Maxxor.PCL.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64Encoded(this string stringToEncode)
        {
            var stringToEncodeBytes = System.Text.Encoding.UTF8.GetBytes(stringToEncode);
            return System.Convert.ToBase64String(stringToEncodeBytes);
        }
    }
}