using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to manipulate everything has a relation with databases
    /// </summary>
    public static class _db
    {
        /// <summary>
        /// Query timeout in seconds default 1800s (30min)
        /// </summary>
        public static int QueryTimeout { get; set; } = 1800;

        #region SqlManipulation

        /// <summary>
        /// Get information of a selection query
        /// </summary>
        /// <param name="query">the selection query</param>
        /// <param name="connectionString">connection</param>
        /// <returns>DataTable</returns>
        public static DataTable Query(string query, string connectionString)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var cmd = new SqlCommand(query, con)
            {
                CommandTimeout = QueryTimeout
            };
            var tbl = new DataTable("DataTable");
            tbl.Load(cmd.ExecuteReader());
            con.Close();
            return tbl;
        }
        /// <summary>
        /// Get information of a selection query
        /// </summary>
        /// <param name="query">the selection query</param>
        /// <param name="connectionKey">type of connection</param>
        /// <returns>DataTable</returns>
        public static DataTable Query(string query, ConnectionKeys connectionKey = ConnectionKeys.DevelopmentString) => Query(query, connectionKey.ConnectionString());

        /// <summary>
        /// Select information
        /// </summary>
        /// <param name="table">table name</param>
        /// <param name="fields">fields</param>
        /// <param name="connectionKeys">con type</param>
        /// <returns>DataTable</returns>
        public static DataTable From(string table, string fields = "*", ConnectionKeys connectionKeys = ConnectionKeys.DevelopmentString) => From(table, connectionKeys.ConnectionString(), fields);
        /// <summary>
        /// Select information
        /// </summary>
        /// <param name="table"></param>
        /// <param name="connectionString"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static DataTable From(string table, string connectionString, string fields) => Query($"SELECT {fields} FROM {table}", connectionString);
        /// <summary>
        /// Select information
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fields">fields to select</param>
        /// <returns>DataTable</returns>
        public static DataTable From<T>(string fields = "*") => From(_attr.GetName<T>(), fields,_attr.GetConnectionKey<T>());
        /// <summary>
        /// Select information
        /// </summary>
        /// <param name="conditions">conditions</param>
        /// <param name="table">table name</param>
        /// <param name="fields">fields to select</param>
        /// <param name="connectionKeys">con type</param>
        /// <returns>DataTable</returns>
        public static DataTable Where(string conditions, string table, string fields = "*", ConnectionKeys connectionKeys = ConnectionKeys.DevelopmentString) => Query($"SELECT {fields} FROM {table} WHERE {conditions}", connectionKeys);
        /// <summary>
        /// Select information
        /// </summary>
        /// <param name="conditions">conditions</param>
        /// <param name="fields">fields to select</param>
        /// <returns>DataTable</returns>
        public static DataTable Where<T>(string conditions, string fields = "*") => Where(conditions, _attr.GetName<T>(), fields, _attr.GetConnectionKey<T>());

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="query">the query</param>
        /// <param name="connectionString">connection</param>
        /// <returns>value pk of inserted element</returns>
        public static object Exec(string query, string connectionString)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var isInsert = query.ToLower().Trim().IndexOf("insert", StringComparison.Ordinal) == 0;
            if (isInsert) query += ";SELECT SCOPE_IDENTITY();";
            var cmd = new SqlCommand(query, con)
            {
                CommandTimeout = QueryTimeout
            };
            if (!isInsert) return cmd.ExecuteNonQuery();
            var id = cmd.ExecuteScalar();
            con.Close();
            return id;
        }
        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="query">the query</param>
        /// <param name="connectionKey">type of connection</param>
        /// <returns>value pk of inserted element</returns>
        public static object Exec(string query, ConnectionKeys connectionKey = ConnectionKeys.DevelopmentString) => Exec(query, connectionKey.ConnectionString());
        /// <summary>
        /// Execute a transaction query
        /// </summary>
        /// <param name="query">the query</param>
        /// <param name="connectionString">connection</param>
        /// <returns>value pk of inserted element</returns>
        public static object TransExec(string query, string connectionString)
        {
            query = $@"
                SET XACT_ABORT ON
                BEGIN TRAN;
	                {query}
                COMMIT TRAN;
            ";
            return Exec(query, connectionString);
        }
        /// <summary>
        /// Execute a transactional query
        /// </summary>
        /// <param name="query">the query</param>
        /// <param name="connectionKey">type of connection</param>
        /// <returns>value pk of inserted element</returns>
        public static object TransExec(string query, ConnectionKeys connectionKey = ConnectionKeys.DevelopmentString) => TransExec(query, connectionKey.ConnectionString());
        /// <summary>
        /// Get first result of a query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static object FirstResOfQuery(string query, string connectionString)
        {
            using var con = new SqlConnection(connectionString);
            con.Open();
            var cmd = new SqlCommand(query, con) { CommandTimeout = QueryTimeout };
            var res = cmd.ExecuteScalar();
            con.Close();
            return res ;
        }

        /// <summary>
        /// count results of a select query
        /// </summary>
        /// <param name="table">the name of table</param>
        /// <param name="condition">conditions</param>
        /// <param name="connectionKey">type of connection</param>
        /// <returns></returns>
        public static int Count(string table, string condition = null, ConnectionKeys connectionKey = ConnectionKeys.DevelopmentString) => Count(table, connectionKey.ConnectionString(), condition);
        /// <summary>
        /// count results of a select query
        /// </summary>
        /// <param name="table">the name of table</param>
        /// <param name="condition">conditions</param>
        /// <param name="connectionString">type of connection</param>
        /// <returns></returns>
        public static int Count(string table, string connectionString, string condition = null)
        {
            var req = condition.IsNotNull() ? $"SELECT COUNT(*) FROM {table} WHERE {condition}" : $"SELECT COUNT(*) FROM {table}";
            return FirstResOfQuery(req, connectionString).ToInt();
        }
        /// <summary>
        /// count results of a select query
        /// </summary>
        /// <typeparam name="T">type of class</typeparam>
        /// <param name="condition">condition</param>
        /// <returns>int</returns>
        public static int Count<T>(string condition = null) => Count(_attr.GetName<T>(), condition, _attr.GetConnectionKey<T>());
        /// <summary>
        /// Check if a query has results
        /// </summary>
        /// <typeparam name="T">type of class</typeparam>
        /// <param name="condition">the name of table</param>
        /// <returns>boolean value</returns>
        public static bool Exists<T>(string condition = null) => Count<T>(condition) > 0;
        /// <summary>
        /// count results of a select query
        /// </summary>
        /// <param name="table">the name of table</param>
        /// <param name="condition">conditions</param>
        /// <param name="connectionString">connectionString</param>
        /// <returns></returns>
        public static bool Exists(string table, string connectionString, string condition = null) => Count(table, connectionString, condition) > 0;

        #endregion

        #region SqlConversion

        /// <summary>
        /// Get sql date
        /// </summary>
        /// <param name="date">date to verify</param>
        /// <param name="hasTimestamp">if HasTimestamp</param>
        /// <returns>string sql date</returns>
        public static string ToSqlDate(this object date, bool hasTimestamp = false)
        {
            if (!date.IsDate()) return "NULL";
            var dt = date.ToDate();
            return $"'{dt.Format($"yyyyMMdd{(hasTimestamp ? " HH:mm:ss.fff" : "")}")}'";
        }
        /// <summary>
        /// Sql boolean value
        /// </summary>
        /// <param name="value">value to verify</param>
        /// <returns>sql string boolean value</returns>
        public static string ToSqlBool(this object value)
        {
            if (value.IsNull()) return "0";
            if (value.Is("true") || value.Is("oui")) return "1";
            if (value.Is("false") || value.Is("non")) return "0";
            return "CONVERT(Bit, '" + value + "')";
        }
        /// <summary>
        /// get sql numeric format
        /// </summary>
        /// <param name="numeric">value</param>
        /// <returns>numeric format</returns>
        public static string ToSqlNumber(this object numeric) => numeric.GetNumericFormat().Item1.Replace(',','.');
        /// <summary>
        /// get sql string format
        /// </summary>
        /// <param name="str">the value</param>
        /// <returns>sql string</returns>
        public static string ToSqlString(this object str) => str.IsNotNull() ? string.Concat("'", str.ToString().Trim().Replace("'", "''"), "'") : "NULL";
        /// <summary>
        /// Get the sql value from a value given
        /// </summary>
        /// <param name="value">the given value</param>
        /// <returns>sql string</returns>
        public static string ToSqlValue(this object value)
        {
            if (value.IsNumeric()) return value.ToSqlNumber();
            if (value.Is("true") || value.Is("false")) return value.ToSqlBool();
            return value.IsDate() ? value.ToSqlDate() : value.ToSqlString();
        }

        #endregion

        /// <summary>
        /// Check table existence
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TableExists(string tableName, string connectionString) => Exists("dbo.sysobjects", connectionString, $"[name] = {tableName.ToSqlString()} AND [type] = 'U'");
        /// <summary>
        /// DropTable
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="connectionString"></param>
        public static void DropTable(string tableName, string connectionString) => Exec($"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE [name] = '{tableName}' AND [type] = 'U') DROP TABLE [{tableName}] ;", connectionString);
        /// <summary>
        /// Get all databases
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static DataTable AllDatabases(string connectionString) => Query($"SELECT name,name 'Libellé' FROM master.sys.databases WHERE Cast(CASE WHEN name IN ('master', 'model', 'msdb', 'tempdb') THEN 1 ELSE is_distributor END As bit) = 0 ORDER BY name", connectionString);
        /// <summary>
        /// Get top zero dbs
        /// </summary>
        /// <returns></returns>
        public static DataTable EmptyDatabases() => Query($"SELECT top(0) 'name' name, 'Libellé' Libellé");
        /// <summary>
        /// Replace Condition Keywords in a query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="whereClause"></param>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public static string ReplaceConditionKeyword(string query, string whereClause = null, string currentUser = null)
        {
            query = query.Replace("{User}", currentUser.IsNull() ? "''" : currentUser.ToSqlString());
            var hasCondKeyword = query.Contains("@Condition") || query.Contains("@CondWithWhere") || query.Contains("@CondWithoutWhere");
            if (whereClause.IsNotNull())
            {
                var isProcedure = query.Trim().StartsWith("EXEC", StringComparison.OrdinalIgnoreCase);
                if (isProcedure)
                {
                    query = query.Trim().Trim(',');
                    if (query.Has("@")) query += ",";
                    query += $" {whereClause}";
                }
                else
                {
                    if (hasCondKeyword)
                    {
                        query = query.Replace("@Condition", $" {whereClause} ");
                        query = query.Replace("@CondWithWhere", $" AND {whereClause} ");
                        query = query.Replace("@CondWithoutWhere", $" WHERE {whereClause} ");
                    }
                    else query += $" WHERE {whereClause}";
                }
            }
            else if (hasCondKeyword)
            {
                query = query.Replace("@Condition", "");
                query = query.Replace("@CondWithWhere", "");
                query = query.Replace("@CondWithoutWhere", "");
            }
            return query;
        }
    }
    /// <summary>
    /// Proc params class
    /// </summary>
    public class ProcParams
    {
        /// <summary>
        /// ProcName
        /// </summary>
        public string Name;
        private string _type;
        /// <summary>
        /// Type of param
        /// </summary>
        public ProcParamType Type()
        {
            switch (_type.ToLower())
            {
                case "bigint":
                    return ProcParamType.BigInt;
                case "binary":
                    return ProcParamType.Binary;
                case "bit":
                    return ProcParamType.Bit;
                case "char":
                    return ProcParamType.Char;
                case "date":
                    return ProcParamType.Date;
                case "datetime":
                    return ProcParamType.DateTime;
                case "datetime2":
                    return ProcParamType.DateTime2;
                case "decimal":
                    return ProcParamType.Decimal;
                case "float":
                    return ProcParamType.Float;
                case "tinyint":
                    return ProcParamType.TinyInt;
                case "smallint":
                    return ProcParamType.SmallInt;
                case "int":
                    return ProcParamType.Int;
                case "money":
                    return ProcParamType.Money;
                case "nchar":
                    return ProcParamType.NChar;
                case "ntext":
                    return ProcParamType.NText;
                case "numeric":
                    return ProcParamType.Numeric;
                case "nvarchar":
                    return ProcParamType.NVarchar;
                case "real":
                    return ProcParamType.Real;
                case "varchar":
                    return ProcParamType.VarChar;
                case "text":
                    return ProcParamType.Text;
                case "variant":
                    return ProcParamType.Variant;
                default:
                    return ProcParamType.Variant;
            }
        }
        /// <summary>
        /// Length of param
        /// </summary>
        public int Length;
        /// <summary>
        /// Precision of param
        /// </summary>
        public int Precision;
        /// <summary>
        /// Scale of param
        /// </summary>
        public int Scale;
        /// <summary>
        /// Order of param
        /// </summary>
        public int Order;
        /// <summary>
        /// Collation of param
        /// </summary>
        public string Collation;

        /// <summary>
        /// Get params by proc name
        /// </summary>
        /// <param name="procName">proc name</param>
        /// <param name="connectionKey">connectionKey</param>
        /// <returns>List of ProcParams</returns>
        public static (List<ProcParams>,bool) Get(string procName, ConnectionKeys connectionKey) => Get(procName, connectionKey.ConnectionString());

        /// <summary>
        /// Get params by proc name
        /// </summary>
        /// <param name="procName">proc name</param>
        /// <param name="connectionString">connectionString</param>
        /// <returns>List of ProcParams and boolean of existence of rowscount</returns>
        public static (List<ProcParams>, bool) Get(string procName, string connectionString)
        {
            var dt = _db.Query($@"
            select  
               'name'   = name,  
               'type'   = type_name(user_type_id),  
               'length' = max_length,
               'prec'   = case when type_name(system_type_id) = 'uniqueidentifier' then precision else OdbcPrec(system_type_id, max_length, precision) end,
               'scale'  = OdbcScale(system_type_id, scale),
               'order'  = parameter_id,  
               'collation'   = convert(sysname, case when system_type_id in (35, 99, 167, 175, 231, 239) then ServerProperty('collation') end)  
              from sys.parameters where object_id = object_id('{procName}') AND name <> '@rowscount'", connectionString);
            return ((from DataRow dr in dt.Rows
                select new ProcParams
                {
                    Name = dr.StringValueOf("name").TrimStart('@'),
                    _type = dr.StringValueOf("type"),
                    Length = dr.IntegerValueOf("length"),
                    Precision = dr.IntegerValueOf("prec"),
                    Scale = dr.IntegerValueOf("scale"),
                    Order = dr.IntegerValueOf("Order"),
                    Collation = dr.StringValueOf("Collation")
                }).ToList(), _db.Exists("sys.parameters", connectionString, $"object_id = object_id('{procName}') AND [name] = '@rowscount'"));
        }
    }

    /// <summary>
    /// Type of a proc parameter
    /// </summary>
    public enum ProcParamType
    {
        /// <summary>
        /// BigInt type
        /// </summary>
        BigInt,
        /// <summary>
        /// Binary Type
        /// </summary>
        Binary,
        /// <summary>
        /// Bit type
        /// </summary>
        Bit,
        /// <summary>
        /// Char type
        /// </summary>
        Char,
        /// <summary>
        /// Date type
        /// </summary>
        Date,
        /// <summary>
        /// DateTime type
        /// </summary>
        DateTime,
        /// <summary>
        /// DateTime2 type
        /// </summary>
        DateTime2,
        /// <summary>
        /// Decimal type
        /// </summary>
        Decimal,
        /// <summary>
        /// Float type
        /// </summary>
        Float,
        /// <summary>
        /// TinyInt type
        /// </summary>
        TinyInt,
        /// <summary>
        /// SmallInt type
        /// </summary>
        SmallInt,
        /// <summary>
        /// Int type
        /// </summary>
        Int,
        /// <summary>
        /// Money Type
        /// </summary>
        Money,
        /// <summary>
        /// Money Type
        /// </summary>
        NChar,
        /// <summary>
        /// Money Type
        /// </summary>
        NText,
        /// <summary>
        /// Numeric Type
        /// </summary>
        Numeric,
        /// <summary>
        /// NVarchar Type
        /// </summary>
        NVarchar,
        /// <summary>
        /// Real type
        /// </summary>
        Real,
        /// <summary>
        /// VarChar type
        /// </summary>
        VarChar,
        /// <summary>
        /// Text type
        /// </summary>
        Text,
        /// <summary>
        /// Other type
        /// </summary>
        Variant
    }
}
