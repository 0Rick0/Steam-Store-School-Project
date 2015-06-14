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
    /// Search something
    /// </summary>
    public partial class search : System.Web.UI.Page
    {
        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //check if the variables are there
            if (string.IsNullOrEmpty(Request.QueryString["q"]))
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("No search query!"));
            }

            //make regex readable query
            // ReSharper disable once PossibleNullReferenceException already checked above
            var regexQuery = "(" + Server.UrlDecode(Request.QueryString["q"]).Replace(" ", ")|(") + ")";

            //ask database
            using (var con = DbProvider.GetOracleConnection())
            {
                using (var com = con.CreateCommand())
                {
                    com.CommandText = "SELECT a.appid, appname " +
                                      "FROM app a, (SELECT appid, listagg(ai.title ||' ' || ai.text) WITHIN GROUP(ORDER BY ai.appid) as descconc " +
                                      "FROM APPSTOREITEM ai " +
                                      "GROUP BY appid) dc " +
                                      "WHERE a.appid = dc.appid " +
                                      "AND REGEXP_SUBSTR(a.appid || categorieid || appname || dc.descconc,:qer,1,1,'i') IS NOT NULL";
                    var pQ = com.CreateParameter();
                    pQ.Direction = ParameterDirection.Input;
                    pQ.DbType = DbType.String;
                    pQ.ParameterName = "qer";
                    pQ.Value = regexQuery;
                    com.Parameters.Add(pQ);

                    var r = com.ExecuteReader();
                    while (r.Read())
                    {
                        //fill view
                        var lc = new LiteralControl();
                        lc.Text = string.Format("<div class=\"searchResult\"><a href=\"/game.aspx?appId={0}\">{1}</a></div>", r["appid"], r["appname"]);
                        innerContent.Controls.Add(lc);
                    }
                }

                using (var com = con.CreateCommand())
                {
                    com.CommandText = "SELECT userid, username " +
                                      "FROM steam_user " +
                                      "WHERE REGEXP_SUBSTR(username||userid,:qer,1,1,'i') IS NOT NULL";
                    var pQ = com.CreateParameter();
                    pQ.Direction = ParameterDirection.Input;
                    pQ.DbType = DbType.String;
                    pQ.ParameterName = "qer";
                    pQ.Value = regexQuery;
                    com.Parameters.Add(pQ);

                    var r = com.ExecuteReader();

                    if (r.HasRows)
                    {
                        var spacer = new LiteralControl();
                        spacer.Text = string.Format("<h2>Users</h2>");
                        innerContent.Controls.Add(spacer);
                    }

                    while (r.Read())
                    {
                        //fill view
                        var lc = new LiteralControl();
                        lc.Text = string.Format("<div class=\"searchResult\"><a href=\"/profile.aspx?userId={0}\">{1}</a></div>", r["userid"], r["username"]);
                        innerContent.Controls.Add(lc);
                    }
                }
            }
        }
    }
}