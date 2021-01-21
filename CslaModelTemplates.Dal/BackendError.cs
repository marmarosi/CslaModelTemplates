using System;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Represents an error occurred on the backend.
    /// </summary>
    [Serializable]
    public class BackendError
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the error type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the source of error messages.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the summary of error messages.
        /// </summary>
        public string StackTrace { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public BackendError() { }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message to send to the client.</param>
        public BackendError(
            string message
            )
        {
            Message = message;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">The message to send to the client.</param>
        /// <param name="name">The name of the error type.</param>
        public BackendError(
            string message,
            string name
            )
        {
            Message = message;
            Name = name;
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="exception">The exception thrown by the backend.</param>
        /// <param name="summary">The summary of the messages.</param>
        public BackendError(
            Exception exception,
            string summary
            )
        {
            if (exception != null)
            {
                while (exception.InnerException != null)
                    exception = exception.InnerException;
                Message = exception.Message;

                Name = exception is CommandFailedException ?
                    ((CommandFailedException)exception).CommandName + "Exception" :
                    exception.GetType().Name;

                Summary = summary;
                Source = exception.TargetSite.DeclaringType?.FullName;
                StackTrace = exception.StackTrace;
            }
        }

        #endregion
    }
}

