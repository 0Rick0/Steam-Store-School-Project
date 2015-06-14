﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class addFriend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request["friendId"]))
            {
                Response.Redirect("error.aspx?errorMessage="+Server.UrlEncode("No friendId"));
            }
            if (Session["loggedIn"] == null || (bool) Session["loggedIn"]!=true)
            {
                Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Not logged in!"));
            }
            using (var con = DbProvider.GetOracleConnection())
            {
                int userid;
                //get user id
                using (var com = con.CreateCommand())
                {
                    com.CommandText = "SELECT userid FROM steam_user WHERE username = :usrn";
                    var p = com.CreateParameter();
                    p.Direction=ParameterDirection.Input;
                    p.DbType=DbType.String;
                    p.ParameterName = "usrn";
                    p.Value = Session["username"];
                    com.Parameters.Add(p);
                    userid = Convert.ToInt32(com.ExecuteScalar());
                }

                //insert
                using (var com = con.CreateCommand())
                {
                    com.CommandText = "INSERT ALL " +
                                      " INTO friend VALUES(:uid1, :uid2, SYSDATE) " +
                                      " INTO friend VALUES(:uid2, :uid1, SYSDATE)" +
                                      "SELECT * FROM dual";
                    var pUid1 = com.CreateParameter();
                    var pUid2 = com.CreateParameter();
                    pUid1.DbType = pUid2.DbType = DbType.String;
                    pUid1.Direction = pUid2.Direction = ParameterDirection.Input;
                    pUid1.Value = userid;
                    pUid1.ParameterName = "uid1";
                    pUid2.Value = Convert.ToInt32(Request["friendId"]);
                    pUid2.ParameterName = "uid2";
                    com.Parameters.Add(pUid1);
                    com.Parameters.Add(pUid2);
                    if (com.ExecuteNonQuery() <= 0)
                    {
                        Response.Redirect("error.aspx?errorMessage=" + Server.UrlEncode("Somthing whent wrong!"));
                    }
                }
                Response.Redirect("Profile.aspx");
            }
        }
    }
}