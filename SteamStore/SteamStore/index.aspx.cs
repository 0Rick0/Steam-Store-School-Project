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
    /// Index page
    /// </summary>
    public partial class index : System.Web.UI.Page
    {
        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
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

        /// <summary>
        /// Load all the games
        /// </summary>
        /// <param name="id">The category id</param>
        /// <param name="limit">How much to load</param>
        private void LoadGames(int id, int limit = 50)
        {
            var con = DbProvider.GetOracleConnection();
            var com = con.CreateCommand();
            if (id == 1)
            {
                com.CommandText =
@"SELECT a.appId as appId,a.appName as appName,  p.PICID as picId
FROM app a JOIN (
SELECT appid, MIN(picid) as picid
FROM pictures
GROUP BY appId
)p ON a.appId=p.appId
WHERE rownum <= :rn";
            }
            else
            {
                com.CommandText =
@"SELECT a.appId as appId,a.appName as appName,  p.PICID as picId
FROM app a JOIN (
SELECT appid, MIN(picid) as picid
FROM pictures
GROUP BY appId
)p ON a.appId=p.appId
WHERE a.CATEGORIEID = :id
AND rownum <= :rn";
                var pId = com.CreateParameter();
                pId.Value = id;
                pId.ParameterName = "id";
                pId.Direction= ParameterDirection.Input;
                pId.DbType = DbType.Int32;
                com.Parameters.Add(pId);
            }

            var pRn = com.CreateParameter();
            pRn.Value = limit;
            pRn.ParameterName = "rn";
            pRn.Direction = ParameterDirection.Input;
            pRn.DbType = DbType.Int32;

            com.Parameters.Add(pRn);

            var r = com.ExecuteReader();

            while (r.Read())
            {
                var uc = (smallGameView)Page.LoadControl("~/smallGameView.ascx");

                uc.GameId = r["appId"].ToString();
                uc.Title = (string) r["appName"];
                uc.ImageId = r["picId"].ToString();
            
                innerContent.Controls.Add(uc);
                uc.LoadData();
            }
        }
    }
}