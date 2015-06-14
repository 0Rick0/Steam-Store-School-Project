using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;

namespace SteamStore
{
    /// <summary>
    /// Buy a game
    /// </summary>
    public partial class BuyGame : System.Web.UI.Page
    {
        /// <summary>
        /// The pack name
        /// </summary>
        protected string PackName { get; set; }

        /// <summary>
        /// The games of in the pack
        /// </summary>
        protected string Games { get; set; }

        /// <summary>
        /// The price of the pack
        /// </summary>
        protected decimal GamePrice { get; set; }

        /// <summary>
        /// The current user balance
        /// </summary>
        protected decimal Balance { get; set; }

        /// <summary>
        /// Load of page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["loggedIn"]==null || (bool)Session["loggedIn"] != true)
            {
                //not logged in redirect to login page
                Response.Redirect("/Login.aspx?returnUrl="+Server.UrlEncode(Request.Url.ToString()));
            }

            if (string.IsNullOrEmpty(Request.QueryString["packId"]))
            {
                Response.Redirect("/error.aspx?errorMessage="+Server.UrlEncode("No packId specified!"));
            }

            using (var con = DbProvider.GetOracleConnection())
            {
                using (var com = con.CreateCommand())
                {
                    com.CommandText =
                        "SELECT p.packid as pid, p.PACKNAME as pname, p.price*(1-p.discount) as total, listagg(appname,'<br/>') WITHIN GROUP(ORDER BY ap.packid) AS games  " +
                        "FROM pack p, app_pack ap, app a  " +
                        "WHERE ap.appid=a.appid  " +
                        "AND ap.packid = p.packid  " +
                        "AND ap.packid = :pid  " +
                        "GROUP BY p.packid, p.packname, p.price, p.discount";
                    var p = com.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.Int32;
                    p.ParameterName = "pid";
                    p.Value = Request.QueryString["packId"];

                    com.Parameters.Add(p);
                    var r = com.ExecuteReader();
                    r.Read();
                    PackName = (string)r["pname"];
                    Games = (string)r["games"];
                    GamePrice = (decimal) r["total"];
                }

                using (var balCom = con.CreateCommand())
                {
                    balCom.CommandText = "SELECT balance " +
                                         "FROM steam_user " +
                                         "WHERE username = :usr";
                    var p = balCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "usr";
                    p.Value = (string)Session["username"];
                    balCom.Parameters.Add(p);

                    Balance = Convert.ToDecimal((float)balCom.ExecuteScalar());
                }

                if(Balance-GamePrice<0)
                {
                    balAfter.CssClass = "red";
                }
            }
        }

        /// <summary>
        /// Confirm buy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Unnamed3_Click(object sender, EventArgs e)
        {
            using (var con = DbProvider.GetOracleConnection())
            {
                using (var com = con.CreateCommand())
                {
                    System.Diagnostics.Debug.WriteLine(Request.QueryString["packId"]);
                    com.CommandType = CommandType.StoredProcedure;
                    com.CommandText = "BuyGame";
                    var pPid = com.CreateParameter();
                    pPid.DbType = DbType.String;
                    pPid.Direction = ParameterDirection.Input;
                    pPid.ParameterName = "pids";
                    pPid.Value = Request.QueryString["packId"];

                    var pUid = com.CreateParameter();
                    pUid.DbType = DbType.String;
                    pUid.Direction = ParameterDirection.Input;
                    pUid.ParameterName = "usrn";
                    pUid.Value = (string)Session["username"];

                    com.Parameters.Add(pUid);
                    com.Parameters.Add(pPid);

                    //Buy the games
                    try
                    {
                        com.ExecuteNonQuery();
                    }
                    catch (OracleException oex)
                    {
                        System.Diagnostics.Debug.WriteLine("OE:"+oex.Number+" = "+oex.ToString());
                        if (oex.Number == 20000)
                        {
                            Response.Redirect("/error.aspx?errorMessage="+Server.UrlEncode("Not enough money!"));
                        }

                        return;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                    Response.Redirect("/index.aspx");
                }
            }
        }
    }
}