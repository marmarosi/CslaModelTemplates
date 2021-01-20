using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Common.Models
{
    /// <summary>
    /// Utility to convert entity key arrays.
    /// </summary>
    public static class KeyList
    {
        /// <summary>
        /// Converts a key array to string list.
        /// </summary>
        /// <param name="keys">The array of the entity keys.</param>
        /// <returns>The string list of the keys.</returns>
        public static string ToString(
            long[] keys
            )
        {
            string list = "";

            if (keys == null || keys.Length == 0)
                return list;

            foreach (long key in keys)
                list += "," + key.ToString();

            return list.Substring(1);
        }

        /// <summary>
        /// Converts a string list to key array.
        /// </summary>
        /// <param name="list">The string list of the keys.</param>
        /// <returns>The array of the entity keys.</returns>
        public static long[] ToArray(
            string list
            )
        {
            List<long> keys = new List<long>();

            if (!string.IsNullOrWhiteSpace(list))
            {
                string[] items = list.Split(',');
                foreach (string item in items)
                    keys.Add(Convert.ToInt64(item));
            }
            return keys.ToArray();
        }
    }
}
