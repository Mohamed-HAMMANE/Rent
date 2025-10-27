using System.Collections.Generic;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to manipulate sessions
    /// </summary>
    public static class _session
    {
        private static readonly Dictionary<string, object> Items = new();

        /// <summary>
        /// Store a session
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value">the value</param>
        public static void Set(string key, object value) => Items[key] = value;

        /// <summary>
        /// Store a session
        /// </summary>
        /// <param name="value">the value</param>
        public static void Set<T>(T value) => Set(_attr.GetName<T>(), value);

        /// <summary>
        /// Get a stored session
        /// </summary>
        /// <param name="key">the key of session</param>
        /// <returns>object</returns>
        public static object Get(string key) => !Items.ContainsKey(key) ? null : Items[key];

        /// <summary>
        /// Get a stored session
        /// </summary>
        /// <returns>object</returns>
        public static T Get<T>() => (T)Get(_attr.GetName<T>());
        /// <summary>
        /// If not exists set it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Assert(string key, object value)
        {
            if (Get(key) == null) Set(key, value);
        }
    }
}
