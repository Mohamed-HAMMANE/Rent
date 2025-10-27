using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to get helper functions
    /// </summary>
    public static class _func
    {
        private static readonly Regex RegexSelectDistinct = new("SELECT DISTINCT", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex RegexSelect = new("SELECT", RegexOptions.IgnoreCase | RegexOptions.Compiled);


        /// <summary>
        /// Delete AppSetting
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteAppSetting(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null) return;
            settings.Remove(key);
            configFile.Save(ConfigurationSaveMode.Modified);
        }
        /// <summary>
        /// Get AppSetting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSetting(string key)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var res = configFile.AppSettings.Settings[key]?.Value;
            return res.IsNull() ? null : res;
        }
        /// <summary>
        /// Get AppSetting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Config(string key) => GetAppSetting(key);

        /// <summary>
        /// Get the detail information of an exception
        /// </summary>
        /// <param name="ex">the exception</param>
        /// <returns>detail</returns>
        public static string GetExceptionDetails(this Exception ex)
        {
            var sb = new System.Text.StringBuilder();

            sb.AppendLine("".PadLeft(80, '-'));
            AppendException(ex);
            sb.AppendLine($"###### Date : {_date.Now()}");
            sb.AppendLine("".PadLeft(80, '_'));

            return sb.ToString();

            void AppendException(Exception e, int depth = 0)
            {
                while (true)
                {
                    var indent = new string('>', depth);
                    sb.AppendLine($"{indent} Exception Type: {e.GetType().FullName}");
                    sb.AppendLine($"{indent} Message       : {e.Message}");
                    sb.AppendLine($"{indent} Source        : {e.Source}");
                    sb.AppendLine($"{indent} StackTrace    : {e.StackTrace}");

                    if (e.Data.Count > 0)
                    {
                        sb.AppendLine($"{indent} Data:");
                        foreach (var key in e.Data.Keys) sb.AppendLine($"{indent}  {key} = {e.Data[key]}");
                    }

                    if (e.InnerException == null) return;
                    sb.AppendLine($"{indent} --- Inner Exception ---");
                    e = e.InnerException;
                    depth += 1;
                }
            }
        }

        /// <summary>
        /// .Net type to sql type
        /// </summary>
        /// <param name="netType">System.Boolean for example</param>
        /// <param name="maxLength">maxLength</param>
        /// <returns></returns>
        public static string NetType2SqlType(string netType, int maxLength = 0)
        {
            return netType switch
            {
                "System.Boolean" => "[bit]",
                "System.Byte" => "[tinyint]",
                "System.Int16" => "[smallint]",
                "System.Int32" => "[int]",
                "System.Int64" => "[bigint]",
                "System.Byte[]" => "[varbinary](max)",
                "System.Char[]" => "[char](max)",
                "System.String" => $"[nvarchar] ({(maxLength is <= 0 or > 250 ? "max" : "250")})",
                "System.Single" => "[real]",
                "System.Double" => "[float]",
                "System.Decimal" => "[float]", // Optional: Use "[decimal](18,2)" for more precision
                "System.DateTime" => "[datetime2](7)",
                "System.Guid" => "[uniqueidentifier]",
                "System.Object" => "[sql_variant]",
                _ => throw new ArgumentOutOfRangeException(nameof(netType), $@"Unhandled .NET type: {netType}")
            };
        }

        /// <summary>
        /// NetTypeToSqlDbType
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static SqlDbType NetTypeToSqlDbType(Type type)
        {
            if (type.IsEnum) return SqlDbType.Int;

            return Type.GetTypeCode(type) switch
            {
                TypeCode.Boolean => SqlDbType.Bit,
                TypeCode.Byte => SqlDbType.TinyInt,
                TypeCode.Int16 => SqlDbType.SmallInt,
                TypeCode.Int32 => SqlDbType.Int,
                TypeCode.Int64 => SqlDbType.BigInt,
                TypeCode.Single => SqlDbType.Real,
                TypeCode.Double => SqlDbType.Float,
                TypeCode.Decimal => SqlDbType.Decimal,
                TypeCode.String => SqlDbType.VarChar,
                TypeCode.DateTime => SqlDbType.DateTime,
                _ when type == typeof(Guid) => SqlDbType.UniqueIdentifier,
                _ when type == typeof(byte[]) => SqlDbType.VarBinary,
                _ => SqlDbType.Int
                //_ => SqlDbType.Variant
            };
        }

        /// <summary>
        /// Get values of enum
        /// </summary>
        /// <typeparam name="T">The enum</typeparam>
        /// <returns>IEnumerable</returns>
        public static IEnumerable<T> GetValues<T>() => Enum.GetValues(typeof(T)).Cast<T>();
        /// <summary>
        /// Get default dataTable from query
        /// </summary>
        /// <param name="query">query</param>
        /// <param name="connectionString">connectionKey</param>
        /// <param name="user">current user</param>
        /// <returns>DataTable</returns>
        public static DataTable DefaultDataTable(string query, string connectionString, string user = null)
        {
            if (query.IsNull()) return null;
            var req = _db.ReplaceConditionKeyword(query, null, user);

            if (req.ToLower().StartsWith("exec")) req += (req.Has("@") ? "," : "") + " @rowscount = 100";
            else
            {
                if (req.Trim().ToUpper().StartsWith("SELECT TOP") || req.Trim().ToUpper().StartsWith("SELECT DISTINCT TOP")) return _db.Query(req, connectionString);
                req = req.Trim().ToUpper().StartsWith("SELECT DISTINCT") ? RegexSelectDistinct.Replace(req, "SELECT DISTINCT TOP(100)", 1) : RegexSelect.Replace(req, "SELECT TOP(100)", 1);
            }
            return _db.Query(req, connectionString);
        }
        /// <summary>
        /// Get the main class of a type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type MainClass(this Type type)
        {
            if (!type.IsClass) return null;
            var baseType = type;
            while (true)
            {
                if (baseType?.BaseType is null || baseType.BaseType.IsInstanceOfType(typeof(object))) break;
                baseType = baseType?.BaseType;
            }
            return baseType;
        }

        /// <summary>
        /// Log exceptions
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void Log(this Exception ex) => Log(ex.GetExceptionDetails());

        /// <summary>
        /// Log data
        /// </summary>
        /// <param name="str"></param>
        public static void Log(string str)
        {
            try
            {
                var logFilePath = _file.Log();
                var logFile = new System.IO.FileInfo(logFilePath);
                if (logFile.Exists)
                {
                    var sizeMb = logFile.Length / 1048576;
                    if (sizeMb > 50) logFile.Delete();
                }
                System.IO.File.AppendAllText(logFilePath, str);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Get all combinations of a list of types
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="list">List</param>
        /// <param name="length">length</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> GetKCombs<T>(this IReadOnlyCollection<T> list, int length) where T : IComparable => length == 1 ? list.Select(t => new[] { t }) : GetKCombs(list, length - 1).SelectMany(t => list.Where(o => o.CompareTo(t.Last()) > 0), (t1, t2) => t1.Concat([t2]));
    }
}
