using CslaModelTemplates.Common;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Dal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace CslaModelTemplates.Endpoints
{
    public static class Helper
    {
        /// <summary>
        /// Gets the path of the request.
        /// </summary>
        public static string Uri(
            HttpRequest request
            )
        {
            return request == null ? "" : request.Path.ToString();
        }

        /// <summary>
        /// Handles the eventual exceptions.
        /// </summary>
        /// <param name="exception">The exception thrown by the backend.</param>
        /// <returns>The error information to send to the frontend.</returns>
        public static ObjectResult HandleError(
            ControllerBase endpoint,
            ILogger logger,
            Exception exception
            )
        {
            if (exception is ValidationException)
            {
                // Status code 422 = Unprocesssable Entity
                return endpoint.UnprocessableEntity(new ValidationError((ValidationException)exception));
            }
            else
            {
                Exception ex = exception;
                string prefix = ">>> WebAPI";
                string summary = string.Empty;
                int statusCode = StatusCodes.Status500InternalServerError;

                while (ex != null)
                {
                    string line = "{0} {1} * {2}".With(prefix, ex.GetType().Name, ex.Message);
                    if (ex.Source != null)
                        line += " [ {0} ]".With(ex.Source);
                    Debug.WriteLine(line);

                    if (summary.Length > 0) summary += "\n";
                    summary += line;

                    if (ex is BackendException)
                        statusCode = (ex as BackendException).StatusCode;

                    ex = ex.InnerException;
                    prefix = "        ";
                }
                logger.LogError(exception, summary, null);
                ObjectResult result = new ObjectResult(new BackendError(exception, summary));
                result.StatusCode = statusCode;
                return result;
            }
        }
    }
}
