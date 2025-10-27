using System;
using System.Globalization;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Date class to manipulate dates
    /// </summary>
    public static class _date
    {
        /// <summary>
        /// Default datetime value
        /// </summary>
        public static DateTime DefaultDateValue { get; set; } = new(1900, 1, 1);

        /// <summary>
        /// Check if an object is a date
        /// </summary>
        /// <param name="date">the object</param>
        /// <returns>boolean value</returns>
        public static bool IsDate(this object date)
        {
            if (date == null) return false;
            return DateTime.TryParse(date.ToString(), CultureInfo.CurrentCulture, DateTimeStyles.None, out var dt) &&
                   DateTime.Compare(Convert.ToDateTime(dt, CultureInfo.CurrentCulture), DefaultDateValue) > 0;
        }

        /// <summary>
        /// Convert an object to datetime if not date it returns the current date
        /// </summary>
        /// <param name="handle">the object</param>
        /// <returns>DateTime</returns> 
        public static DateTime ToDate(this object handle) => handle.IsDate() ? Convert.ToDateTime(handle, CultureInfo.CurrentCulture) : DefaultDateValue;
        /// <summary>
        /// Current date
        /// </summary>
        /// <param name="type">type of string of date (dd/MM/yyyy HH:mm:ss)</param>
        /// <returns>string date</returns>
        public static string Now(string type = "dd/MM/yyyy HH:mm:ss") => DateTime.Now.Format(type);
        /// <summary>
        /// Format datetime
        /// </summary>
        /// <param name="dt">DateTime</param>
        /// <param name="format">format string</param>
        /// <returns>datetime formatted</returns>
        public static string Format(this DateTime dt, string format = "dd/MM/yyyy") => dt.ToString(format);
    }
}
