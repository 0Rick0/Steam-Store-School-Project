using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SteamStore
{
    public partial class MyGames : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["username"]==null || string.IsNullOrEmpty((string)Session["username"]))
            {
                Response.Redirect("Login.aspx?returnUrl="+Server.UrlEncode(Request.Url.ToString()));
            }
            var id = 1;
            var limit = 50;
            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
            }
            if (!string.IsNullOrEmpty(Request.QueryString["limit"]))
            {
                limit = Convert.ToInt32(Request.QueryString["limit"]);
            }
            LoadGames(id, limit);
        }
        private void LoadGames(int id, int limit = 50)
        {
            var con = DbProvider.GetOracleConnection();
            var com = con.CreateCommand();
            
            com.CommandText =
@"SELECT a.appId as appId,a.appName as appName,  p.PICID as picId
FROM app a JOIN (
    SELECT appid, MIN(picid) as picid
    FROM pictures
    GROUP BY appId)p ON a.appId=p.appId
WHERE a.appId IN(
    SELECT appId
    FROM bought b, steam_user u
    WHERE b.userid = u.userid
    AND u.username = :usrn)
AND rownum <= :rn";
            

            var pRn = com.CreateParameter();
            pRn.Value = limit;
            pRn.ParameterName = "rn";
            pRn.Direction = ParameterDirection.Input;
            pRn.DbType = DbType.Int32;

            var pUsrn = com.CreateParameter();
            pUsrn.DbType = DbType.String;
            pUsrn.Direction = ParameterDirection.Input;
            pUsrn.ParameterName = "usrn";
            pUsrn.Value = Session["username"];

            com.Parameters.Add(pUsrn);
            com.Parameters.Add(pRn);
            

            var r = com.ExecuteReader();

            while (r.Read())
            {
                var uc = (smallGameView)Page.LoadControl("~/smallGameView.ascx");


                uc.GameId = r["appId"].ToString();
                uc.Title = (string)r["appName"];
                uc.ImageId = r["picId"].ToString();

                innerContent.Controls.Add(uc);
                uc.LoadData();
            }
        }
    }
}