using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class Profile : System.Web.UI.Page
    {
        protected string Username { get; set; }
        protected decimal Balance { get; set; }
        protected bool IsSelf { get; set; }

        protected int UserId { get; set; }
        protected bool IsFriend { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            using (var con = DbProvider.GetOracleConnection())
            {
                if (string.IsNullOrEmpty(Request.QueryString["userId"]))
                {
                    //login if the user isn't logged in and wants to see own info
                    if (Session["loggedIn"] == null || (bool) Session["loggedIn"] == false)
                    {
                        Response.Redirect("Login.aspx?returnUrl="+Server.UrlEncode(Request.Url.ToString()));
                    }
                    Username = (string)Session["Username"];
                    IsSelf = true;//the add form balance will be displayed
                    IsFriend = true;//nothing will be displayed
                    //get username, userId and balance
                    using (var com = con.CreateCommand())
                    {
                        

                        com.CommandText = "SELECT username, userid, balance " +
                                          "FROM steam_user " +
                                          "WHERE username = :usrid";
                        var p = com.CreateParameter();
                        p.Direction=ParameterDirection.Input;
                        p.DbType = DbType.String;
                        p.Value = Session["username"];
                        p.ParameterName = "usrid";
                        com.Parameters.Add(p);
                        var r = com.ExecuteReader();
                        r.Read();
                        UserId = Convert.ToInt32((long) r["userId"]);
                        Balance = Convert.ToDecimal((float) r["balance"]);
                        Username = (string) r["username"];
                    }
                }
                else
                {
                    //get usrename and id
                    using (var com = con.CreateCommand())
                    {


                        com.CommandText = "SELECT username, userid " +
                                          "FROM steam_user " +
                                          "WHERE userId = :usrid";
                        var p = com.CreateParameter();
                        p.Direction = ParameterDirection.Input;
                        p.DbType = DbType.Int32;
                        p.Value = Request.QueryString["userId"];
                        p.ParameterName = "usrid";
                        com.Parameters.Add(p);
                        var r = com.ExecuteReader();
                        r.Read();
                        UserId = Convert.ToInt32((long)r["userId"]);
                        Username = (string)r["username"];
                    }
                    //check if the user is logged in and if the userid is a friend
                    if (!(Session["loggedIn"] == null || (bool)Session["loggedIn"] == false))
                    {
                        using (var com = con.CreateCommand())
                        {
                            com.CommandText = "SELECT count(*) " +
                                              "FROM steam_user u, friend f " +
                                              "WHERE u.userid = f.userid " +
                                              "AND username = :usrn " +
                                              "AND friendUserId = :usrid";


                            var pusr = com.CreateParameter();
                            pusr.Direction = ParameterDirection.Input;
                            pusr.DbType = DbType.String;
                            pusr.Value = Session["username"];
                            pusr.ParameterName = "usrn";
                            com.Parameters.Add(pusr);

                            var puid = com.CreateParameter();
                            puid.Direction = ParameterDirection.Input;
                            puid.DbType = DbType.Int32;
                            puid.Value = UserId;
                            puid.ParameterName = "usrid";
                            com.Parameters.Add(puid);

                            var cnt = Convert.ToInt32(com.ExecuteScalar());
                            IsFriend = cnt > 0;
                        }
                    }
                    else
                    {
                        IsFriend = true;//nothing will be displayed
                    }
                }
                //get friends
                using (var com = con.CreateCommand())
                {
                    com.CommandText = "SELECT u.username as username, u.userid as userid " +
                                      "FROM steam_user u, friend f " +
                                      "WHERE u.userid = f.FRIENDUSERID " +
                                      "AND f.USERID = :usrid";
                    var p = com.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.Int32;
                    p.Value = UserId;
                    p.ParameterName = "usrid";
                    com.Parameters.Add(p);
                    var r = com.ExecuteReader();
                    while (r.Read())
                    {
                        var lc = new LiteralControl();
                        lc.Text = string.Format(@"<div class=""friend""><a href=""?userId={0}""><h2>{1}</h2></a></div>",r["userid"],r["username"]);
                        friends.Controls.Add(lc);
                    }
                }
                //get achievements
                using (var com = con.CreateCommand())
                {
                    com.CommandText =
                        "SELECT a.title as title, '\"'||a.description ||'\" at '|| TO_CHAR(ua.datetime,'DD/MM/YYYY') as descdate " +
                        "FROM achievement a, user_achievement ua " +
                        "WHERE a.ACHIEVEMENTID = ua.ACHIEVEMENTID " +
                        "AND ua.USERID = :usrid";
                    var p = com.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.Int32;
                    p.Value = UserId;
                    p.ParameterName = "usrid";
                    com.Parameters.Add(p);
                    var r = com.ExecuteReader();
                    while (r.Read())
                    {
                        var lc = new LiteralControl();
                        lc.Text = string.Format(@"<div class=""achievement""><h2>{0}</h2><div>{1}</div></div>", r["title"], r["descdate"]);
                        achievements.Controls.Add(lc);
                    }
                }
                
            }
            
        }
    }
}