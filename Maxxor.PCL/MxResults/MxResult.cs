using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
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

        #region Comine

        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static MxResult Combine(object sender, IEnumerable<MxResult> results, [CallerMemberName] string methodName = "")
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(MxResult), result);
            }

            return Ok();
        }

        /// <summary>
        /// Checks all Results in the list and returns Fail the first time one returns Fail
        /// and return a list of values if all Results succeed
        /// </summary>
        /// <typeparam name="T">Type of object returned by the Results in the list</typeparam>
        /// <param name="sender">this</param>
        /// <param name="results">List of Results of Type T</param>
        /// <returns>List of Result values if all Succeed else Fail</returns>
        public static MxResult<List<T>> Combine<T>(object sender, IEnumerable<MxResult<T>> results, [CallerMemberName] string methodName = "")
        {
            List<T> values = new List<T>();
            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    return Fail<List<T>>(sender, result);
                }
                values.Add(result.Value);
            }
            return Ok(values);
        }

        /// <summary>
        /// Checks all values for failures
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="results"></param>
        /// <returns>Success if all are success else fail with error updated with all failures in order received</returns>
        public static MxResult Combine(object sender, params MxResult[] results)
        {
            foreach (var result in results.Where(result => result.IsFailure))
            {
                return Fail(typeof(MxResult), result);
            }
            return Ok();
        }


        #endregion

        #region Try
        /// <summary>
        /// Wrapper around a void method that may throw an exception and return a Result instead
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static MxResult Try(object sender, Action operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult result;
            try
            {
                operation();
                result = Ok();
            }
            catch (Exception exception)
            {
                return Fail(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }
        
        /// <summary>
        /// Wrapper around a method that takes parameters and that may throw an exception
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static MxResult<T> Try<T>(object sender, Func<T> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult<T> result;
            try
            {
                var returnValue = operation();
                result = Ok(returnValue);
            }
            catch (Exception exception)
            {
                return Fail<T>(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }

        /// <summary>
        /// Wrapper around a method that returns a Task and that may throw an exception
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static async Task<MxResult> Action(object sender, Func<Task> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult result;
            try
            {
                await operation();
                result = Ok();
            }
            catch (Exception exception)
            {
                return Fail(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }

        /// <summary>
        /// Wrapper around a method that returns a Task and has a return value and that may throw an exception
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static async Task<MxResult<T>> Action<T>(object sender, Func<Task<T>> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult<T> result;
            try
            {
                var value = await operation();
                result = Ok(value);
            }
            catch (Exception exception)
            {
                return Fail<T>(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }

        #endregion

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

    /// <summary>
    /// Used to wrap method return values so that calling methods always have a consistent 
    /// way to determine if the called method achieved its intended result and proceed accordingly 
    /// - without having to try and catch exceptions everywhere. 
    /// </summary>
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