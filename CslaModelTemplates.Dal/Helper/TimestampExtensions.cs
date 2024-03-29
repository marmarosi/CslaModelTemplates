using System.Diagnostics;

namespace CslaModelTemplates.Dal.Helper
{
    /// <summary>
    /// This class provides helper methods for SQL Server timestamps.
    /// </summary>
    public static class TimestampExtensions
    {
        /// <summary>
        /// Determines whether two timestamps differ.
        /// </summary>
        /// <param name="timestamp1">The first timestamp.</param>
        /// <param name="timestamp2">The second timestamp.</param>
        /// <returns>True when the timestamps differ; otherwise false.</returns>
        [DebuggerStepThrough]
        public static bool NotEquals(byte[] timestamp1, byte[] timestamp2)
        {
            if (timestamp1.Length != timestamp2.Length)
                return true;

            for (int i = 0; i < timestamp1.Length; i++)
                if (timestamp1[i] != timestamp2[i])
                    return true;

            return false;
        }
    }
}
