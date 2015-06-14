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
            //get an connection and put the connection string in it
            //it shouldn't be null, so if it is an error may be uncaught. this is an fatal exception so it doesn't need to be
            var con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            //open the connection and return it
            con.Open();
            return con;
        }
    }
}