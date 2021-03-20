using CslaModelTemplates.Common;
using CslaModelTemplates.Common.Validations;
using CslaModelTemplates.Dal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Net;

namespace CslaModelTemplates.WebApi
{
    /// <summary>
    /// Serves as the base class for the API controllers.
    /// </summary>
    public class ApiController : ControllerBase
    {
        internal ILogger Logger { get; set; }

        /// <summary>
        /// Gets the path of the request.
        /// </summary>
        protected string Uri
        {
            get
            {
                return Request == null ? "" : Request.Path.ToString();
            }
        }

        /// <summary>
        /// Creates a new instance of the controller.
        /// </summary>
        /// <param name="logger">The application logging service.</param>
        internal ApiController(
            ILogger logger
            )
        {
            Logger = logger;
        }

        /// <summary>
        /// Handles the eventual exceptions.
        /// </summary>
        /// <param name="exception">The exception thrown by the backend.</param>
        /// <returns>The error information to send to the frontend.</returns>
        protected IActionResult HandleError(
            Exception exception
            )
        {
            if (exception is ValidationException)
            {
                // Status code 422 = Unprocesssable Entity
                return StatusCode(422, new ValidationError((ValidationException)exception));
            }
            else
            {
                int statusCode = (int)HttpStatusCode.InternalServerError;
                Exception ex = exception;
                string prefix = ">>> WebAPI";
                string summary = string.Empty;

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
                Logger.LogError(exception, summary, null);
                return StatusCode(statusCode, new BackendError(exception, summary));
            }
        }
    }
}
