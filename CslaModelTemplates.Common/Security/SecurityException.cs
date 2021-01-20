using System;
using System.Net;

namespace CslaModelTemplates.Common.Security
{
    /// <summary>
    /// Represents an exception thrown by security reason.
    /// </summary>
    [Serializable]
    public class SecurityException : BackendException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public SecurityException(
            string message
            )
            : base(message)
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public SecurityException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        {
            StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
