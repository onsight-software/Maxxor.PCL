using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Maxxor.PCL.MxResults.Interfaces;

namespace Maxxor.PCL.MxResults
{
    /// <summary>
    /// Holds caught exceptions and defects and passes them back with metadata up the call stack 
    /// to allow errors to be tracked from the user interface all the way back to the source of the error
    /// </summary>
    public class MxError
    {

        #region Construction

        protected MxError(object sender, IMxErrorCondition errorCondition, Exception exception, [CallerMemberName] string methodName = "")
        {
            ErrorCondition = errorCondition;
            MethodName = methodName;
            ClassName = sender.GetType().Name;
            Exception = exception;
        }

        /// <summary>
        /// Creates a new error either with a caught exception or with a custom error condition 
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="errorCondition">Any implementation of IMxErrorCondition - either from MxErrorCondition or from another class</param>
        /// <param name="exception">If there is an exception associated with this error condition</param>
        /// <param name="methodName">Determined automatically - no need to supply manually</param>
        /// <returns></returns>
        public static MxError Create(object sender, IMxErrorCondition errorCondition, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return new MxError(sender, errorCondition, exception)
            {
                MethodName = methodName
            };
        }

        /// <summary>
        /// Creates a new Error from an existing one as it is passed up the call stack. 
        /// Updates MethodName and PreviousError to maintain the call chain.
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="previousError">Error returned from called function</param>
        /// <param name="methodName">Populated automatically by Update function</param>
        /// <returns></returns>
        public static MxError Update(object sender, MxError previousError, string methodName = "")
        {
            return new MxError(sender, previousError.ErrorCondition, null)
            {
                MethodName = methodName,
                PreviousError = previousError
            };
        }
        
        #endregion

        #region Core Error Data

        /// <summary>
        /// Exposes the Description from the ErrorCondition. 
        /// This is intended to be a human-readable and translatable string to communicate with users. 
        /// </summary>
        public string Description => ErrorCondition.Description;

        /// <summary>
        /// Provides a newline-separated string containing each class and method called between 
        /// the origin of the Error and the current location
        /// </summary>
        public string ErrorList => GetErrorList();
        private string GetErrorList()
        {
            return ErrorStack.Aggregate("", (current, error) => current + $"{error.ClassName}.{error.MethodName}()\n");
        }

        /// <summary>
        /// The error prior to this one in the call stack
        /// </summary>
        public MxError PreviousError { get; protected set; }

        /// <summary>
        /// The original Error at the beginning of the chain
        /// </summary>
        public MxError SourceError => ErrorStack.LastOrDefault();

        /// <summary>
        /// The full list of Errors including this one and all those before it in the chain
        /// </summary>
        public List<MxError> ErrorStack => GetErrorStack(new List<MxError> { this });
        private List<MxError> GetErrorStack(List<MxError> errors)
        {
            if (PreviousError == null) return errors;
            errors.Add(PreviousError);
            PreviousError.GetErrorStack(errors);
            return errors;
        }

        /// <summary>
        /// Identity of the ErrorCondition with a user-readable message
        /// </summary>
        public IMxErrorCondition ErrorCondition { get; set; }

        /// <summary>
        /// The method that created this Error
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// The class that created this Error
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// If an Exception was caught and handled by the Error this will be it.
        /// </summary>
        public Exception Exception { get; }

        #endregion

        #region Additional Error Data

        /// <summary>
        /// Additional metadata to assist with understanding the causes of the Error
        /// </summary>
        public Dictionary<string, string> AdditionalData { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Use to append any descriptive data about the error for logging, debugging and communication purposes.
        /// </summary>
        /// <param name="key">Any string</param>
        /// <param name="value">Any string</param>
        /// <returns></returns>
        public MxError AddData(string key, string value)
        {
            AdditionalData[key] = value;
            return this;
        }

        #endregion

        #region ViewModel Data

        /// <summary>
        /// Returns an anonymous object containing Error particulars that can be passed
        /// to an Error display ViewModel so that the user can be informed about what happened.
        /// </summary>
        public object ViewModelParameters => new
        {
            ErrorCondition = ErrorCondition.ToString(),
            description = Description,
            exceptionMessage = SourceError.Exception != null ? SourceError.Exception.Message : "",
            exceptionStackTrace = SourceError.Exception != null ? SourceError.Exception.StackTrace : "",
            innerExceptionMessage = SourceError.Exception?.InnerException != null ? SourceError.Exception.InnerException.Message : "",
            errorStack = GetErrorList()
        };

        #endregion

    }

}