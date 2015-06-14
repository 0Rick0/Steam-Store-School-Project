using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    /// <summary>
    /// Add balance to an account
    /// </summary>
    public partial class addBalance : System.Web.UI.Page
    {
        /// <summary>
        /// When the page is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the nedded variables are available
            if (string.IsNullOrEmpty(Request["addbalance"]))
            {
                Response.Redirect("error.aspx?errorMessage="+Server.UrlEncode("No Balance!"));
            }

            if (Session["loggedIn"] == null || (bool) Session["loggedIn"] == false)
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Not lgged in!"));
            }

            decimal toadd;
            if (!decimal.TryParse(Request["addbalance"], out toadd))
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Not a number, "+Request["addbalance"]));
            }

            //update the balance
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
                        //if somthing when wrong show error
                        Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Nothing added!"));
                    }
                }
            }

            //go back to profile
            Response.Redirect("Profile.aspx");
        }
    }
}