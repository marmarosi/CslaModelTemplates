using System;

namespace CslaModelTemplates.Dal
{
    /// <summary>
    /// Represents an exception when the execution of a command failed.
    /// </summary>
    [Serializable]
    public class CommandFailedException : DalException
    {
        public string CommandName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public CommandFailedException(
            string message,
            string commandName
            )
            : base(message)
        {
            CommandName = commandName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandFailedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CommandFailedException(
            string message,
            string commandName,
            Exception innerException
            )
            : base(message, innerException)
        {
            CommandName = commandName;
        }
    }
}
