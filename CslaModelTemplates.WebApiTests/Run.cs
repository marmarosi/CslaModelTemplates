using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace CslaModelTemplates.WebApiTests
{
    public static class Run
    {
        public static async Task<IActionResult> OnDeadlockRetry(
            Func<Task<IActionResult>> businessMethod,
            int maxRetries = 3
            )
        {
            int retryCount = 0;

            while (retryCount < maxRetries)
            {
                try
                {
                    return await businessMethod();
                }
                catch (SqlException e) // This example is for SQL Server, change the exception type/logic if you're using another DBMS
                {
                    if (e.Number == 1205)  // SQL Server error code for deadlock
                    {
                        retryCount++;
                        Thread.Sleep(100);
                    }
                    else
                    {
                        throw e;  // Not a deadlock so throw the exception
                    }
                    // Add some code to do whatever you want with the exception once you've exceeded the max. retries
                }
            }
            return default(IActionResult);
        }
    }
}
