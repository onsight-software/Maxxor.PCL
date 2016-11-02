using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Maxxor.PCL.MxResults.Interfaces;

namespace Maxxor.PCL.MxResults
{
    /// <summary>
    /// Used to wrap method return values so that calling methods always have a consistent 
    /// way to determine if the called method achieved its intended result and proceed accordingly 
    /// - without having to try and catch exceptions everywhere. 
    /// </summary>
    public class MxResult
    {

        #region Properties

        /// <summary>
        /// The called method achieved the expected result. No exceptions or undesirable events happened. 
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// The method did not succeed in its goal. Perhaps an exception was thrown, or a known 
        /// and expected deviation occured (eg incorrect password) that needs to be dealt with by the caller
        /// </summary>
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// If the Result IsFailure, it will have an Error attached to it containing details of the problem
        /// </summary>
        public MxError Error { get; }

        #endregion

        #region Ok

        /// <summary>
        /// Creates a new Result with IsSuccess true
        /// </summary>
        /// <returns></returns>
        public static MxResult Ok()
        {
            return new MxResult(true, null);
        }
        /// <summary>
        /// Creates a new Result with IsSuccess true and Value set
        /// </summary>
        /// <returns></returns>
        public static MxResult<T> Ok<T>(T value)
        {
            return new MxResult<T>(value, true, null);
        }

        #endregion
        
        #region Fail

        /// <summary>
        /// Creates a new Result from a received Result so that it can be updated and passed up the chain.
        /// Error stacktrace is updated from the sender
        /// </summary>
        /// <param name="sender">Class that is receiving the Result</param>
        /// <param name="previousResult">Result returned from called method</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static MxResult Fail(object sender, MxResult previousResult, [CallerMemberName] string methodName = "")
        {
            return Fail(MxError.Update(sender, previousResult.Error, methodName));
        }

        /// <summary>
        /// Creates a new Result with IsSuccess false and ErrorCondition set. 
        /// Exception can also be provided if there is one. 
        /// </summary>
        /// <param name="sender">The class in which the Error occured (this)</param>
        /// <param name="errorCondition">Contains error name and user-friendly message</param>
        /// <param name="exception">Optional</param>
        /// <param name="methodName">Automatic</param>
        /// <returns></returns>
        public static MxResult Fail(object sender, IMxErrorCondition errorCondition, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return Fail(MxError.Create(sender, errorCondition, exception, methodName));
        }

        /// <summary>
        /// Creates a new Result from a received Result so that it can be updated and passed up the chain.
        /// Error stacktrace is updated from the sender
        /// </summary>
        /// <param name="sender">Class that is receiving the Result</param>
        /// <param name="previousResult">Result returned from called method</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static MxResult<T> Fail<T>(object sender, MxResult previousResult, [CallerMemberName] string methodName = "")
        {
            return Fail<T>(MxError.Update(sender, previousResult.Error, methodName));
        }

        /// <summary>
        /// Creates a new Result with IsSuccess false and ErrorCondition set. 
        /// Exception can also be provided if there is one. 
        /// </summary>
        /// <param name="sender">The class in which the Error occured (this)</param>
        /// <param name="errorCondition">Contains error name and user-friendly message</param>
        /// <param name="exception">Optional</param>
        /// <param name="methodName">Automatic</param>
        /// <returns></returns>
        public static MxResult<T> Fail<T>(object sender, IMxErrorCondition errorCondition, Exception exception = null, [CallerMemberName] string methodName = "")
        {
            return Fail<T>(MxError.Create(sender, errorCondition, exception, methodName));
        }



        #endregion

        #region Return

        public static MxResult Return(object sender, MxResult previousResult)
        {
            return previousResult.IsSuccess
                ? Ok()
                : Fail(sender, previousResult);
        }

        public static MxResult<T> Return<T>(object sender, MxResult<T> previousResult)
        {
            return previousResult.IsSuccess
                ? Ok(previousResult.Value)
                : Fail<T>(sender, previousResult);
        }

        #endregion


        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results"></param>
        /// <returns>List of type values if all Succeed else Fail with error updated with all failures in order received</returns>
        public static MxResult<List<T>> Combine<T>(IEnumerable<MxResult<T>> results)
        {
            List<T> values = new List<T>();
            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    return Fail<List<T>>(typeof(MxResult), result);
                }
                values.Add(result.Value);
            }
            return Ok(values);
        }

        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static MxResult Combine(params MxResult[] results)
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(MxResult), result);
            }

            return Ok();
        }

        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static MxResult Combine(IEnumerable<MxResult> results)
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(MxResult), result);
            }

            return Ok();
        }

        #region Privates

        protected MxResult(bool isSuccess, MxError error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        protected static MxResult Fail(MxError error)
        {
            return new MxResult(false, error);
        }

        protected static MxResult<T> Fail<T>(MxError error)
        {
            return new MxResult<T>(default(T), false, error);
        }

        #endregion

    }
    #region ResultOfType

    public class MxResult<T> : MxResult
    {
        public T Value { get; }
        public MxResult(T value, bool isSuccess, MxError error) : base(isSuccess, error)
        {
            Value = value;
        }
    }

    #endregion
}