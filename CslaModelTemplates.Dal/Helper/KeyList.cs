using System;
using System.Collections.Generic;

namespace CslaModelTemplates.Dal.Helper
{
    /// <summary>
    /// Utility to convert entity key arrays and lists.
    /// </summary>
    public static class KeyList
    {
        /// <summary>
        /// Converts a key list to string.
        /// </summary>
        /// <param name="keys">The list of the entity keys.</param>
        /// <returns>The string of the keys.</returns>
        public static string ToString(
            List<long> keys
            )
        {
            return ToString(keys.ToArray());
        }

        /// <summary>
        /// Converts a key array to string list.
        /// </summary>
        /// <param name="keys">The array of the entity keys.</param>
        /// <returns>The string of the keys.</returns>
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
        /// Converts a string to key array.
        /// </summary>
        /// <param name="list">The string of the keys.</param>
        /// <returns>The array of the entity keys.</returns>
        public static long[] ToArray(
            string list
            )
        {
            return ToList(list).ToArray();
        }

        /// <summary>
        /// Converts a string to key list.
        /// </summary>
        /// <param name="list">The string of the keys.</param>
        /// <returns>The list of the entity keys.</returns>
        public static List<long> ToList(
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
            return keys;
        }
    }
}
