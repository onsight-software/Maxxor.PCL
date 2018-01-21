using System;
namespace Maxxor.PCL.Result
{
    public struct MxErrorDetails
    {
        public string ErrorType { get; set; }
        public string Description { get; set; }
        public string ErrorStack { get; set; }
        public string InnerExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public string ExceptionMessage { get; set; }
    }
}
