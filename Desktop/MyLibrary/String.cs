using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to manipulate strings
    /// </summary>
    public static class _str
    {
        /// <summary>
        /// Default value to return when an error occurred
        /// </summary>
        public static int DefaultNumberValue { get; set; }

        #region Convertion to number

        /// <summary>
        /// Convert a value to decimal format
        /// </summary>
        /// <param name="val">the value</param>
        public static string ToDecimalFormat(this object val) => val.ToDecimal().ToString("n");
        /// <summary>
        /// Convert an object to decimal
        /// </summary>
        /// <param name="val">the object</param>
        /// <returns></returns>
        public static decimal ToDecimal(this object val) => decimal.TryParse(val.GetNumericFormat().Item1, out var res) ? res : DefaultNumberValue;
        /// <summary>
        /// Convert an object to int
        /// </summary>
        /// <param name="val">the object</param>
        /// <returns></returns>
        public static int ToInt(this object val) => int.TryParse(val.GetNumericFormat().Item1, out var res) ? res : DefaultNumberValue;
        /// <summary>
        /// Parse string to int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ParseInt(this string val) => int.Parse(val);
        /// <summary>
        /// Parse object to int
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static int ParseInt(this object val) => val.ToString().ParseInt();
        /// <summary>
        /// Convert an object to long
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static long ToLong(this object val) => long.TryParse(val.GetNumericFormat().Item1, out var res) ? res : DefaultNumberValue;
        /// <summary>
        /// Convert an object to double
        /// </summary>
        /// <param name="val">the object</param>
        /// <returns></returns>
        public static double ToDouble(this object val) => double.TryParse(val.GetNumericFormat().Item1, out var res) ? res : DefaultNumberValue;

        #endregion

        #region Checks

        /// <summary>
        /// Check if an string is null
        /// </summary>
        /// <param name="str"></param>
        /// <returns>bool</returns>
        public static bool IsNull(this string str)
        {
            if (str == null) return true;
            str = str.Trim();
            if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str)) return true;
            return str == "" || str.Equals("NULL", StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// Check if an string is not null
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNotNull(this string str) => !str.IsNull();
        /// <summary>
        /// Check if an element is null
        /// </summary>
        /// <param name="obj">the element</param>
        /// <returns>boolean value</returns>
        public static bool IsNull(this object obj)
        {
            switch (obj)
            {
                case null:
                    return true;
                case DateTime when !obj.IsDate():
                    return true;
                default:
                    return obj.ToString().IsNull();
            }
        }

        /// <summary>
        /// Check if an element is not null
        /// </summary>
        /// <param name="obj">the element</param>
        /// <returns>boolean value</returns>
        public static bool IsNotNull(this object obj) => !obj.IsNull();

        /// <summary>
        /// Check if an object is numeric
        /// </summary>
        /// <param name="obj">the object</param>
        public static bool IsNumeric(this object obj)
        {
            return GetNumericFormat(obj).Item2;
            /*if (obj.IsNull()) return false;
            if (obj is decimal || obj is int || obj is double) return true;
            var nbr = obj.ToString().Replace(',', '.');
            return decimal.TryParse(nbr, out _) || int.TryParse(nbr, out _) || double.TryParse(nbr, out _);*/
        }
        /// <summary>
        /// Get number format to be converted
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>the number format if number else default value</returns>
        public static (string,bool) GetNumericFormat(this object obj)
        {
            try
            {
                if (obj == null) return (DefaultNumberValue.ToString(), false);

                var str = obj.ToString().Trim();
                if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str))
                    return (DefaultNumberValue.ToString(), false);

                // Fast path: try direct parse first
                if (decimal.TryParse(str, out _)) return (str, true);

                // Handle European vs US format
                var lastComma = str.LastIndexOf(',');
                var lastDot = str.LastIndexOf('.');

                if (lastComma > lastDot)
                {
                    // European format: 1.234,56 -> 1234.56
                    str = str.Replace(".", "").Replace(",", ".");
                }
                else if (lastDot >= 0)
                {
                    // US format: 1,234.56 -> 1234.56
                    str = str.Replace(",", "");
                }

                // Remove spaces
                if (str.IndexOf(' ') >= 0)
                    str = str.Replace(" ", "");

                // Trim extra dots/commas
                str = str.Trim('.', ',');

                // Final parse attempt
                if (decimal.TryParse(str, out _)) return (str, true);
            }
            catch (Exception)
            {
                // ignored
            }
            return (DefaultNumberValue.ToString(), false);
        }

        /// <summary>
        /// Check if a string contains a handle string
        /// </summary>
        /// <param name="str">the main string</param>
        /// <param name="handle">the handle</param>
        /// <returns>boolean value</returns>
        public static bool Has(this object str, string handle) => str != null && handle != null && str.ToString().IndexOf(handle, StringComparison.OrdinalIgnoreCase) != -1 && str.ToString().ToLower().Contains(handle.ToLower());
        /// <summary>
        /// Check if a string equals another
        /// </summary>
        /// <param name="str">the first string</param>
        /// <param name="handle">the second string</param>
        /// <returns>boolean value</returns>
        public static bool Is(this object str, string handle) => str != null && handle != null && str.ToString().Equals(handle, StringComparison.OrdinalIgnoreCase);

        #endregion

        #region Others

        /// <summary>
        /// Get unique id
        /// </summary>
        /// <returns>string unique id</returns>
        public static string UniqueId() => Guid.NewGuid().ToString("N");
        /// <summary>
        /// Remove Diacritics from string
        /// </summary>
        /// <param name="text">string</param>
        /// <param name="andSpaces">remove spaces</param>
        /// <returns>formatted string</returns>
        public static string RemoveDiacritics(this string text, bool andSpaces = false)
        {
            if (andSpaces) text = text.ToLower().Replace(' ', '_');
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();
            foreach (var c in from c in normalizedString let unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c) where unicodeCategory != UnicodeCategory.NonSpacingMark select c) stringBuilder.Append(c);
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        /// <summary>
        /// First char to lower
        /// </summary>
        /// <param name="text">the value</param>
        /// <returns></returns>
        public static string FirstCharToLower(this string text) => text[0].ToString().ToLower() + text.Substring(1);

        #endregion
    }
}
