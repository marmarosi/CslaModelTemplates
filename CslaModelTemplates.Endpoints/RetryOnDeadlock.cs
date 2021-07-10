using CslaModelTemplates.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace CslaModelTemplates.Endpoints
{
    internal static class RUN
    {
        internal const int MAX_RETRIES = 1;
        internal const int MIN_DELAY_MS = 100;
        internal const int MAX_DELAY_MS = 200;
    }

    public static class Run
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        public async static Task<ActionResult> RetryOnDeadlock(
            Func<Task<ActionResult>> businessMethod,
            int maxRetries = RUN.MAX_RETRIES
            )
        {
            var retryCount = 0;
            ActionResult result = null;

            while (retryCount < maxRetries)
            {
                result = await businessMethod();

                if ((result as OkObjectResult) != null &&
                    (result as OkObjectResult)?.Value is DeadlockError)
                {
                    retryCount++;
                    result = null;
                    Thread.Sleep(_random.Next(RUN.MIN_DELAY_MS, RUN.MAX_DELAY_MS));
                }
                else
                    break;
            }

            return result;
        }
    }

    public static class Call<T> where T : class
    {
        private static readonly Random _random = new Random(DateTime.Now.Millisecond);

        public async static Task<ActionResult<T>> RetryOnDeadlock(
            Func<Task<ActionResult<T>>> businessMethod,
            int maxRetries = RUN.MAX_RETRIES
            )
        {
            var retryCount = 0;
            ActionResult<T> result = null;

            while (retryCount < maxRetries)
            {
                result = await businessMethod();

                if ((result.Result as ObjectResult) != null &&
                    (result.Result as ObjectResult).Value is DeadlockError)
                {
                    retryCount++;
                    result = null;
                    Thread.Sleep(_random.Next(RUN.MIN_DELAY_MS, RUN.MAX_DELAY_MS));
                }
                else
                    break;
            }

            return result;
        }
    }
}
