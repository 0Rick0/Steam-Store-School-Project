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
    /// Display a game
    /// </summary>
    public partial class Game : System.Web.UI.Page
    {
        /// <summary>
        /// The number of images
        /// </summary>
        protected int Imagescnt { get; private set; }

        /// <summary>
        /// The name of the game
        /// </summary>
        protected string GameName { get; private set; }

        /// <summary>
        /// The category of the game
        /// </summary>
        protected string GameCategorie { get; private set; }

        /// <summary>
        /// When the page loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["appId"]))
                {
                    Response.Redirect("/error.aspx?errorMessage="+Server.UrlEncode("No appId specified!"));//show error if querystring is wrong
                    return;
                }
                    
                var con = DbProvider.GetOracleConnection();

                //appId parameter
                
                //load game info
                #region load info
                using (var infoCom = con.CreateCommand())
                {
                    infoCom.CommandText = "SELECT appid, a.categorieid, appname, categoriename " +
                                          "FROM app a, categorie c " +
                                          "WHERE  a.categorieid = c.categorieid " +
                                          "AND a.appid = :aid";
                    var p = infoCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "aid";
                    p.Value = Request.QueryString["appId"];
                    infoCom.Parameters.Add(p);

                    using (var r = infoCom.ExecuteReader())
                    {
                        if (!r.Read())
                        {
                            Response.Redirect("/error.aspx?errorMessage=" + Server.UrlEncode("appId not found!"));//show error if querystring is wrong
                            return;
                        }

                        GameName = (string)r["appName"];
                        GameCategorie = (string)r["categoriename"];
                    }
                }
                #endregion

                //load images
                #region load images
                using (var picCom = con.CreateCommand())
                {
                    picCom.CommandText = "SELECT appid, picid " +
                                      "FROM pictures " +
                                      "WHERE appid = :aid";

                    var p = picCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "aid";
                    p.Value = Request.QueryString["appId"];

                    picCom.Parameters.Add(p);

                    var r = picCom.ExecuteReader();
                    var lc = new LiteralControl();
                    Imagescnt = 0;
                    while (r.Read())
                    {
                        Imagescnt++;
                        lc.Text += string.Format(
                            "<div class=\"imgContainer\">" +
                            "<img src=\"/images/games/{0}.png\" alt=\"Test Image\" />" +
                            "</div>",
                            r["picId"]);
                    }

                    imagesContainer.Controls.Add(lc);
                }
                #endregion
                //add the packs you can buy
                #region load packs
                using (var packCom = con.CreateCommand())
                {
                    packCom.CommandText = "SELECT p.packid as pid, p.PACKNAME as pname,p.description as pdesc, p.price as pprice, p.discount as pdisc, listagg(appname,', ') WITHIN GROUP(ORDER BY ap.packid) AS games " +
                                          "FROM pack p, app_pack ap, app a " +
                                          "WHERE ap.appid=a.appid " +
                                          "AND ap.packid = p.packid " +
                                          "AND ap.packid IN( " +
                                          "  SELECT packid " +
                                          "  FROM app_pack " +
                                          "  WHERE appid = :aid) " +
                                          "GROUP BY p.packid, p.packname, p.description, p.price, p.discount";
                    var p = packCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "aid";
                    p.Value = Request.QueryString["appId"];

                    packCom.Parameters.Add(p);

                    var r = packCom.ExecuteReader();
                    while (r.Read())
                    {
                        var pack = (gameViewPack)Page.LoadControl("~/gameViewPack.ascx");
                        pack.PackTitle = (string) r["pname"];
                        pack.PackId = (int) r["pid"];
                        pack.NewPrice = ((float) r["pprice"] * (1 - (float) r["pdisc"]) ).ToString("f2")+"€";
                        if ((float) r["pdisc"] > 0)
                        {
                            pack.OldPrice = ((float) r["pprice"]).ToString("f2") + "€";
                            pack.Discount = (float) r["pdisc"]*100 + "%";
                        }

                        pack.PackGames = (string) r["pdesc"] + "<br/>" + (string) r["games"];

                        leftContent.Controls.Add(pack);
                    }
                }
                #endregion
                //load description(s)
                #region load description(s)
                using (var descCom = con.CreateCommand())
                {
                    descCom.CommandText = "SELECT title, text "+
                                          "FROM appstoreitem "+
                                          "WHERE appid = :aid "+
                                          "ORDER BY pos";
                    var p = descCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "aid";
                    p.Value = Request.QueryString["appId"];

                    descCom.Parameters.Add(p);

                    var r = descCom.ExecuteReader();
                    while (r.Read())
                    {
                        var uc = new LiteralControl();
                        uc.Text = string.Format(@"<div class=""descriptionitem""><h2>{0}</h2>{1}</div>",r["title"],r["text"]);
                        leftContent.Controls.Add(uc);
                    }
                }
                #endregion
                //load comments
                #region load comments(s)
                using (var comCom = con.CreateCommand())
                {
                    comCom.CommandText = "SELECT u.userId as userId, username as username, text as text " + 
                                         "FROM user_comment uc, steam_user u "+
                                         "WHERE uc.userid = u.userid " +
                                         "AND appId = :aid";
                    var p = comCom.CreateParameter();
                    p.Direction = ParameterDirection.Input;
                    p.DbType = DbType.String;
                    p.ParameterName = "aid";
                    p.Value = Request.QueryString["appId"];

                    comCom.Parameters.Add(p);
                    var lc = new LiteralControl();
                    lc.Text += @"<div class=""descriptionitem""><h2>Comments</h2>";
                    var r = comCom.ExecuteReader();
                    while (r.Read())
                    {
                        lc.Text += string.Format(
                            @"<div class=""commentContainer"">
    <a href=""/user.aspx?userId={0}"" class=""commentUser"">{1}</a>
    <div class=""commentText"">
        <p>
            {2}
        </p>
    </div>
</div>",
                            r["userId"],
                            r["username"],
                            r["text"]);
                    }

                    lc.Text += "</div>";
                    leftContent.Controls.Add(lc);
                }
                #endregion
            }
        }
    }
}