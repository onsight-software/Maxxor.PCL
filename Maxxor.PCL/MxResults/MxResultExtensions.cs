namespace Maxxor.PCL.MxResults
{
    public static class MxResultExtensions
    {
        public static MxResult AddData(this MxResult result, string key, string value)
        {
            result.Error.AddData(key, value);
            return result;
        }

        public static MxResult<T> AddData<T>(this MxResult<T> result, string key, string value)
        {
            result.Error.AddData(key, value);
            return result;
        }
    }
}