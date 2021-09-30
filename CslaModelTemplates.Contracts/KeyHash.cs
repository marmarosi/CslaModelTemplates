﻿using HashidsNet;
using System.Collections.Generic;

namespace CslaModelTemplates.Contracts
{
    /// <summary>
    /// Provides methods to obfuscate keys.
    /// </summary>
    public static class KeyHash
    {
        private static Dictionary<string, Hashids> _hashids = new Dictionary<string, Hashids>();

        private static Hashids GetHashids(
            string model
            )
        {
            Hashids hashids;
            if (!_hashids.TryGetValue(model, out hashids))
            {
                hashids = new Hashids($"a-{ model }-Z", 11);
                _hashids.Add(model, hashids);
            }
            return hashids;
        }

        /// <summary>
        /// Encodes the provided key into a hash string.
        /// </summary>
        /// <param name="model">The type of the business model.</param>
        /// <param name="key">The key of the business object.</param>
        /// <returns>The hash ID.</returns>
        public static string Encode(
            string model,
            long? key
            )
        {
            var hashids = GetHashids(model);
            return hashids.EncodeLong(key ?? 0);
        }

        /// <summary>
        /// Decodes the provided hash string into key.
        /// </summary>
        /// <param name="model">The type of the business model.</param>
        /// <param name="hashid">The hash ID.</param>
        /// <returns>The key of the business object.</returns>
        public static long? Decode(
            string model,
            string hashid
            )
        {
            var hashids = GetHashids(model);
            var key = hashids.DecodeLong(hashid)[0];
            return key == 0 ? null : (long?)key;
        }
    }
}
