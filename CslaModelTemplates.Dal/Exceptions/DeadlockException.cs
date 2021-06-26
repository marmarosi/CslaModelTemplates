using System;
using System.Net;

namespace CslaModelTemplates.Dal.Exceptions
{
    /// <summary>
    /// Represents an exception when the requested persistent store is locked.
    /// </summary>
    [Serializable]
    public class DeadlockException : DalException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeadlockException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public DeadlockException(
            string message
            )
            : base(message)
        {
            StatusCode = (int)HttpStatusCode.Locked;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeadlockException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DeadlockException(
            string message,
            Exception innerException
            )
            : base(message, innerException)
        {
            StatusCode = (int)HttpStatusCode.Locked;
        }
    }
}
