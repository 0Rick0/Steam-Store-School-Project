using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class addBalance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["addbalance"]))
            {
                Response.Redirect("error.aspx?errorMessage="+Server.UrlEncode("No Balance!"));
            }
            if (Session["loggedIn"] == null || (bool) Session["loggedIn"]==false)
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Not lgged in!"));
            }
            decimal toadd;
            if (!decimal.TryParse(Request["addbalance"], out toadd))
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Not a number, "+Request["addbalance"]));
            }
            using (var con = DbProvider.GetOracleConnection())
            {
                using (var com = con.CreateCommand())
                {
                    com.CommandText = "UPDATE steam_user SET balance = balance + :addb WHERE username = :usrn";
                    var pBal = com.CreateParameter();
                    pBal.Direction = ParameterDirection.Input;
                    pBal.DbType = DbType.Decimal;
                    pBal.Value = toadd;
                    pBal.ParameterName = "addb";
                    com.Parameters.Add(pBal);
                    var pUsrn = com.CreateParameter();
                    pUsrn.Direction = ParameterDirection.Input;
                    pUsrn.DbType = DbType.String;
                    pUsrn.Value = Session["username"];
                    pUsrn.ParameterName = "usrn";
                    com.Parameters.Add(pUsrn);
                    var rows = com.ExecuteNonQuery();
                    if (rows < 1)
                    {
                        Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Nothing added!"));
                    }
                }
            }
            Response.Redirect("Profile.aspx");
        }
    }
}