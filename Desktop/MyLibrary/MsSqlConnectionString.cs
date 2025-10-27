using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Rent.MyLibrary
{
    /// <summary>
    /// Class to manage connection strings
    /// </summary>
    public class MsSqlConnectionString
    {
        /// <summary>
        /// Server name
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Remote server name
        /// </summary>
        public string RemoteServer { get; set; }
        /// <summary>
        /// Database name
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// user id
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MsSqlConnectionString()
        {

        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        public MsSqlConnectionString(string password, string database, string server = ".", string user = "sa")
        {
            Server = server;
            User = user;
            Database = database;
            Password = password;
        }
        /// <summary>
        /// Constructor with ConnectionKey param
        /// </summary>
        /// <param name="connectionKey">connectionKey</param>
        public MsSqlConnectionString(string connectionKey)
        {
            var connection = _connectionString.ConnectionString(connectionKey);
            if (connection.IsNull()) return;
            var val = connection.Split(';');
            foreach (var v in val)
            {
                var values = v.Split('=');
                string key = values[0].Trim(), value = values[1].Trim();
                if (key.Is("server")) Server = value;
                else if (key.Is("database")) Database = value;
                else if (key.Is("user id")) User = value;
                else if (key.Is("password")) Password = value;
            }
        }
        /// <summary>
        /// Constructor with ConnectionKeys param
        /// </summary>
        /// <param name="connectionKey">connectionKey</param>
        public MsSqlConnectionString(ConnectionKeys connectionKey)
        {
            var tmp = new MsSqlConnectionString(connectionKey.ToString());
            if(tmp.IsNull()) return;
            User = tmp.User;
            Server = tmp.Server;
            Password = tmp.Password;
            Database = tmp.Database;
        }

        /// <summary>
        /// Convert to connectionString
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            if(User.IsNotNull() && Password.IsNotNull()) return $"server={Server};database={Database};Min Pool Size=10;Max Pool Size=20;user id={User};Password={Password};connection timeout=10";
            return $"Server={Server};Database={Database};Min Pool Size=10;Max Pool Size=20;Trusted_Connection=True;connection timeout=10";
        }
        /// <summary>
        /// Check if a connection string is working
        /// </summary>
        /// <returns>bool</returns>
        public bool Check() => _connectionString.Check(new MsSqlConnectionString(Password, Database, Server, User).ToString());
    }
    /// <summary>
    /// 
    /// </summary>
    public static class _connectionString
    {
        /// <summary>
        /// ParseConnectionString
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        /// <returns></returns>
        public static MsSqlConnectionString ParseConnectionString(string connectionString)
        {
            if (connectionString.IsNull()) return null;
            var obj = new MsSqlConnectionString();
            var val = connectionString.Split(';');
            foreach (var v in val)
            {
                var values = v.Split('=');
                string key = values[0].Trim(), value = values[1].Trim();
                if (key.Is("server")) obj.Server = value;
                else if (key.Is("database")) obj.Database = value;
                else if (key.Is("user id")) obj.User = value;
                else if (key.Is("password")) obj.Password = value;
            }
            return obj;
        }
        /// <summary>
        /// Get connection string
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>string</returns>
        public static string ConnectionString(string key)
        {
            var connection = ConfigurationManager.ConnectionStrings[key]?.ConnectionString;
            if (connection.IsNull())
            {
                var tmp = _session.Get(key);
                return ((MsSqlConnectionString)tmp)?.ToString();
            }

            return connection;
            /*if ()
            {
                if (!connection.Has("server") && !connection.Has("database") && !connection.Has("password")) connection = _str.Pull(connection);
            }
            return connection;*/
        }



        /// <summary>
        /// Get or set current production connectionString
        /// </summary>
        public static MsSqlConnectionString CurrentProdConnection
        {
            get
            {
                var tmp = _session.Get(nameof(ConnectionKeys.ProductionString));
                if (tmp != null) return (MsSqlConnectionString)tmp;
                var obj = new MsSqlConnectionString(ConnectionKeys.ProductionString);
                _session.Set(nameof(ConnectionKeys.ProductionString), obj);
                return obj;
            }
            set => _session.Set(nameof(ConnectionKeys.ProductionString), value);
        }

        /// <summary>
        /// Get or set current production connectionString
        /// </summary>
        public static MsSqlConnectionString CurrentDevConnection
        {
            get
            {
                var tmp = _session.Get(nameof(ConnectionKeys.DevelopmentString));
                if (tmp != null) return (MsSqlConnectionString)tmp;
                var obj = new MsSqlConnectionString(ConnectionKeys.DevelopmentString);
                _session.Set(nameof(ConnectionKeys.DevelopmentString), obj);
                return obj;
            }
            set => _session.Set(nameof(ConnectionKeys.DevelopmentString), value);
        }



        /// <summary>
        /// Get connection string
        /// </summary>
        /// <param name="connectionKey">key</param>
        /// <returns>string</returns>
        public static string ConnectionString(this ConnectionKeys connectionKey)
        {
            switch (connectionKey)
            {
                /*case ConnectionKeys.ProdSyncString:
                {
                    var con = CurrentProdConnection;
                    return !con.Database.StartsWith("BI_") ? new MsSqlConnectionString(con.Password, $"BI_{con.Database}", con.Server, con.User).ToString() : con.ToString();
                }*/
                case ConnectionKeys.ProductionString: return CurrentProdConnection.ToString();
                case ConnectionKeys.DevelopmentString: return CurrentDevConnection.ToString();
                default: return ConnectionString(connectionKey.ToString());
            }
        }
        /// <summary>
        /// Check if a connection string is working
        /// </summary>
        /// <param name="connectionString">connectionString</param>
        /// <returns>bool</returns>
        public static bool Check(string connectionString)
        {
            try
            {
                connectionString = connectionString.Replace("timeout=30", "timeout=5");
                var sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Check if a connection string is working
        /// </summary>
        /// <param name="connectionKey">connectionKey</param>
        /// <returns>bool</returns>
        public static bool Check(this ConnectionKeys connectionKey)
        {
            var val = ConnectionString(connectionKey.ToString());
            return !val.IsNull() && Check(val);
        }
    }

    /// <summary>
    /// Type of connection keys
    /// </summary>
    public enum ConnectionKeys
    {
        /// <summary>
        /// Key of development database
        /// </summary>
        DevelopmentString,
        /// <summary>
        /// Key of production database
        /// </summary>
        ProductionString
    }
}