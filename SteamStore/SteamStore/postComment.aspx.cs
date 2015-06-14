using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class postComment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //check appid and comment
            if (string.IsNullOrEmpty(Request["appId"]) || string.IsNullOrEmpty(Request["comment"]))
            {
                Response.Redirect("/error.aspx?errorMessage=" + Server.UrlEncode("appId or comment not specified!"));
            }

            //check loggedIn
            if (Session["loggedIn"] == null || (bool) Session["loggedIn"] == false)
            {
                Response.Redirect("/error.aspx?errorMessage=" + Server.UrlEncode("Not logged in!"));
            }

            using (var con = DbProvider.GetOracleConnection())
            {
                using (var com = con.CreateCommand())
                {
                    //todo max + 1 should be replaced with a sequence, this doesn't like mvcc = double id's by way of personal view per connection
                    com.CommandText = "INSERT INTO user_comment " +
                                      "VALUES ((SELECT max(commentid)+1 FROM user_comment), :apid, (SELECT userid FROM steam_user WHERE username=:usrn), :pcomment)";

                    var pApid = com.CreateParameter();
                    pApid.ParameterName = "apid";
                    pApid.Direction=ParameterDirection.Input;
                    pApid.DbType=DbType.Int32;
                    pApid.Value = Convert.ToInt32(Request["appId"]);

                    var pUsrn = com.CreateParameter();
                    pUsrn.ParameterName = "usrn";
                    pUsrn.Direction = ParameterDirection.Input;
                    pUsrn.DbType = DbType.String;
                    pUsrn.Value = (string)Session["username"];

                    var pComment = com.CreateParameter();
                    pComment.ParameterName = "pcomment";
                    pComment.Direction=ParameterDirection.Input;
                    pComment.DbType=DbType.String;
                    pComment.Value = Request["comment"].Replace("\n", "<br/>");

                    com.Parameters.Add(pApid);
                    com.Parameters.Add(pUsrn);
                    com.Parameters.Add(pComment);

                    if (com.ExecuteNonQuery() < 1)
                    {
                        //if less then one row is inserted then somthing is wrong
                        Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Somthing went wrong while executing"));
                    }

                }
                //go back to the game page
                Response.Redirect("Game.aspx?appId="+Request["appId"]);
            }
        }
    }
}