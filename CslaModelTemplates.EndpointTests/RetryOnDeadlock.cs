using CslaModelTemplates.Dal.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CslaModelTemplates.EndpointTests
{
    public static class Run
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        public async static Task<ActionResult> RetryOnDeadlock(
            Func<Task<ActionResult>> businessMethod,
            int maxRetries = 3
            )
        {
            var retryCount = 0;
            ActionResult result = null;

            while (retryCount < maxRetries)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    result = await businessMethod();
                    scope.Dispose();
                }

                if ((result as OkObjectResult) == null &&
                    (result as OkObjectResult)?.Value is DeadlockException)
                {
                    retryCount++;
                    result = null;
                    Console.Beep();
                    Thread.Sleep(_random.Next(100, 200));
                }
                else
                    retryCount = maxRetries;
            }

            return result;
        }
    }

    public static class Call<T> where T: class
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        public async static Task<ActionResult<T>> RetryOnDeadlock(
            Func<Task<ActionResult<T>>> businessMethod,
            int maxRetries = 3
            )
        {
            var retryCount = 0;
            ActionResult<T> result = null;

            while (retryCount < maxRetries)
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    result = await businessMethod();
                    scope.Dispose();
                }

                if ((result.Result as ObjectResult) != null &&
                    (result.Result as ObjectResult).Value is DeadlockException)
                {
                    retryCount++;
                    result = null;
                    Thread.Sleep(_random.Next(100, 200));
                }
                else
                    retryCount = maxRetries;
            }

            return result;
        }
    }
}
