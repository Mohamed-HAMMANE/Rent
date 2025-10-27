using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to manipulate DataTable things
    /// </summary>
    public static class _dt
    {
        /// <summary>
        /// Get value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(object)</returns>
        public static object ValueOf(this DataRow dr, string field) => dr[field];

        /// <summary>
        /// Get string value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(string)</returns>
        public static string StringValueOf(this DataRow dr, string field) => dr.ValueOf(field)?.ToString();
        /// <summary>
        /// Get Integer value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(integer)</returns>
        public static int IntegerValueOf(this DataRow dr, string field) => dr.ValueOf(field).ToInt();
        /// <summary>
        /// Get Long value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(long)</returns>
        public static long LongValueOf(this DataRow dr, string field) => dr.ValueOf(field).ToLong();
        /// <summary>
        /// Get Double value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(double)</returns>
        public static double DoubleValueOf(this DataRow dr, string field) => dr.ValueOf(field).ToDouble();
        /// <summary>
        /// Get decimal value of a fields from a DataRow
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static decimal DecimalValueOf(this DataRow dr, string field) => dr.ValueOf(field).ToDecimal();
        /// <summary>
        /// Get Boolean value of a fields from a DataRow
        /// </summary>
        /// <param name="dr">the DataRow</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(boolean)</returns>
        public static bool BooleanValueOf(this DataRow dr, string field) => dr.ValueOf(field).IsNotNull() && (bool)dr.ValueOf(field);

        //-----------------------------------------------------------

        /// <summary>
        /// Get value of a fields from a DataTable
        /// </summary>
        /// <param name="dt">the DataTable</param>
        /// <param name="field">the name of field</param>
        /// <returns>the value(object)</returns>
        public static object ValueOf(this DataTable dt, string field)
        {
            if (dt == null || dt.Rows.Count <= 0) return null;
            return dt.Rows[0].ValueOf(field);
        }


        /// <summary>
        /// Convert schema DataTable to sql table
        /// </summary>
        /// <param name="dt">the DataTable</param>
        /// <param name="connection">connection</param>
        /// <param name="tableName">table Name</param>
        /// <param name="createPk"></param>
        /// <param name="autoIncrement"></param>
        public static void CopyToSql(this DataTable dt, string connection, string tableName = null, bool createPk = true, bool autoIncrement = true)
        {
            tableName ??= dt.TableName;
            tableName = tableName.Trim('[').Trim(']');
            var sqlCmd = new StringBuilder();
            sqlCmd.AppendLine($"IF EXISTS (SELECT * FROM dbo.sysobjects WHERE [name] = '{tableName}' AND [type] = 'U') DROP TABLE [{tableName}] ;");
            sqlCmd.AppendLine($"CREATE TABLE [{tableName}] (");
            string pk = null;
            foreach (DataColumn col in dt.Columns)
            {
                sqlCmd.Append($"    [{col.ColumnName}] ");
                sqlCmd.Append($"{_func.NetType2SqlType(col.DataType.ToString(), col.MaxLength)} ");
                if (col.AutoIncrement && autoIncrement)
                {
                    sqlCmd.Append("IDENTITY(1,1) ");
                    pk = col.ColumnName;
                }
                sqlCmd.Append((col.AllowDBNull ? "" : "NOT ") + $"NULL,{Environment.NewLine}");
            }
            if (pk != null && createPk) sqlCmd.AppendLine($"    CONSTRAINT [PK_{tableName}] PRIMARY KEY CLUSTERED ([{pk}] ASC),");
            sqlCmd.Remove(sqlCmd.Length - (Environment.NewLine.Length + 1), 1);
            sqlCmd.AppendLine(") ON [PRIMARY];");
            _db.Exec(sqlCmd.ToString(), connection);
        }
        /// <summary>
        /// Copy data to sql table
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="con">connection</param>
        /// <param name="tableName">table Name</param>
        public static void CopyDataToSql(this DataTable dt, string con, string tableName = null)
        {
            tableName ??= dt.TableName;
            tableName = tableName.Trim('[').Trim(']');
            using var connection = new SqlConnection(con);
            connection.Open();
            using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, null))
            {
                foreach (DataColumn c in dt.Columns) bulkCopy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                bulkCopy.DestinationTableName = $"[{tableName}]";
                bulkCopy.BulkCopyTimeout = 0;
                bulkCopy.WriteToServer(dt);
            }
            connection.Close();
        }

        /// <summary>
        /// Create sql table with inserts from DataTable
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="connection">connection</param>
        /// <param name="tableName">table Name</param>
        /// <param name="createPk"></param>
        /// <param name="autoIncrement"></param>
        public static void ToSql(this DataTable dt, string connection, string tableName, bool createPk = true, bool autoIncrement = true)
        {
            tableName ??= dt.TableName;
            tableName = tableName.Trim('[').Trim(']');
            dt.CopyToSql(connection, tableName, createPk, autoIncrement);
            dt.CopyDataToSql(connection, tableName);
        }
    }
}
