using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Web;
using Oracle.ManagedDataAccess.Client;

namespace SteamStore
{
    public static class DbProvider
    {
        public static DbConnection GetOracleConnection()
        {
            var con = OracleClientFactory.Instance.CreateConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["OracleConnection"].ConnectionString;
            con.Open();
            return con;
        }
    }
}