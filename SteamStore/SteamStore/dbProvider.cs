using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace SteamStore
{
    /// <summary>
    /// Providers access to the database
    /// </summary>
    public static class DbProvider
    {
        /// <summary>
        /// Gets an open Oracle connection
        /// </summary>
        /// <returns>The connection</returns>
        public static DbConnection GetOracleConnection()
        {
            var con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            con.Open();
            return con;
        }
    }
}