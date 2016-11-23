using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Maxxor.PCL.Result.Interfaces;

namespace Maxxor.PCL.Result
{
    public static class MxTry
    {

        #region Try Synchronous Methods

        /// <summary>
        /// Wrapper around a void method that may throw an exception and return a Result instead
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static MxResult GetResult(object sender, Action operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult result;
            try
            {
                operation();
                result = MxResult.Ok();
            }
            catch (Exception exception)
            {
                return MxResult.Fail(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
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
        public static MxResult<T> GetResult<T>(object sender, Func<T> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult<T> result;
            try
            {
                var returnValue = operation();
                result = MxResult.Ok(returnValue);
            }
            catch (Exception exception)
            {
                return MxResult.Fail<T>(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }

        #endregion

        #region Try Asynchronous Methods

        /// <summary>
        /// Wrapper around a method that returns a Task and that may throw an exception
        /// </summary>
        /// <param name="sender">The class calling the method</param>
        /// <param name="operation">Method that may thrown an exception</param>
        /// <param name="errorCondition">ErrorCondition to label the error with if an exception is thrown</param>
        /// <param name="methodName">Populated automatically</param>
        /// <returns></returns>
        public static async Task<MxResult> GetResultAsync(object sender, Func<Task> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult result;
            try
            {
                await operation();
                result = MxResult.Ok();
            }
            catch (Exception exception)
            {
                return MxResult.Fail(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
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
        public static async Task<MxResult<T>> GetResultAsync<T>(object sender, Func<Task<T>> operation, IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            MxResult<T> result;
            try
            {
                var returnValue = await operation();
                result = MxResult.Ok(returnValue);
            }
            catch (Exception exception)
            {
                return MxResult.Fail<T>(sender, errorCondition ?? MxErrorCondition.Unspecified, exception, methodName);
            }
            return result;
        }

        #endregion

        #region Try and Retry Asynchronous Methods

        /// <summary>
        /// Tries an async method that may throw an exception a given number of times before 
        /// returning a Fail if the method threw an exception each time it was retried.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender">The class calling the method (this)</param>
        /// <param name="operation">Async Method with return value</param>
        /// <param name="numberOfTimesToTry">Number of times to try the operation before failing</param>
        /// <param name="retryDelayMs">Length of pause between retries in milliseconds</param>
        /// <param name="errorCondition">ErroCondition to attach to the Failure if the retries don't succeed</param>
        /// <param name="methodName">Set automatically</param>
        /// <returns></returns>
        public static async Task<MxResult<T>> GetRetryResult<T>(object sender, Func<Task<T>> operation, int numberOfTimesToTry, int retryDelayMs = 0, 
            IMxErrorCondition errorCondition = null, [CallerMemberName] string methodName = "")
        {
            List<Exception> exceptions = new List<Exception>();
            for (int i = 0; i < numberOfTimesToTry; i++)
            {
                try
                {
                    var returnValue = await operation();
                    return MxResult.Ok(returnValue);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
                await Task.Delay(retryDelayMs);
            }
            var result =  MxResult.Fail<T>(sender, errorCondition ?? MxErrorCondition.Unspecified, new AggregateException(exceptions).Flatten(), methodName);
            result.Error.AddData("tries", $"Tried operation {numberOfTimesToTry} times every {retryDelayMs} milliseconds");
            return result;
        }

        
        #endregion

    }
}